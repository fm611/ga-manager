
using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace GAManager.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const string VirtualHostName = "groupaddress.app";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private static readonly JsonSerializerOptions RecentFilesJsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    };

    // Messages sent by the React app via window.chrome.webview.postMessage(...)
    private class BridgeMessage
    {
        public string? Id { get; set; }
        public string Type { get; set; } = "";
        public string? Content { get; set; }
        public bool? Dirty { get; set; }
        public string? Path { get; set; }
        public string? FileName { get; set; }
    }

    private readonly ConcurrentDictionary<string, TaskCompletionSource<string?>> _pendingHostRequests = new();
    private readonly string _recentFilesPath = Path.Combine(AppContext.BaseDirectory, "recent.json");
    private List<FileInfo> _recentFiles = [];

    private FileInfo? _currentFile;
    private bool _dirty;
    private bool _forceClose;
    private string? _pendingOpenPath;

    static MainWindow()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public MainWindow()
    {
        InitializeComponent();

        _pendingOpenPath = GetProjectFileFromCommandLine();
        ReadRecentFilesList();
        UpdateTitle();

        SourceInitialized += (_, _) => ApplyTitleBarTheme();
        Loaded += MainWindow_Loaded;
    }

    // Recolors the native title bar to match the React app's dark KNX theme
    // (colorNeutralBackground1 / colorNeutralForeground1 from GAManager.Web/src/theme.ts).
    private void ApplyTitleBarTheme()
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        if (hwnd == IntPtr.Zero) return;

        var background = ToColorRef((Color)ColorConverter.ConvertFromString("#292929"));
        var foreground = ToColorRef(Colors.White);
        var useDarkMode = 1;

        DwmSetWindowAttribute(hwnd, DwmwaUseImmersiveDarkMode, ref useDarkMode, sizeof(int));
        DwmSetWindowAttribute(hwnd, DwmwaCaptionColor, ref background, sizeof(int));
        DwmSetWindowAttribute(hwnd, DwmwaTextColor, ref foreground, sizeof(int));
        DwmSetWindowAttribute(hwnd, DwmwaBorderColor, ref background, sizeof(int));
    }

    private static int ToColorRef(Color c) => c.R | (c.G << 8) | (c.B << 16);

    private const int DwmwaUseImmersiveDarkMode = 20;
    private const int DwmwaBorderColor = 34;
    private const int DwmwaCaptionColor = 35;
    private const int DwmwaTextColor = 36;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int valueSize);

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await InitializeWebViewAsync();
    }

    private static string? GetProjectFileFromCommandLine()
    {
        var args = Environment.GetCommandLineArgs();
        for (int i = 1; i < args.Length; i++)
        {
            if (string.Equals(Path.GetExtension(args[i]), ".gaproj", StringComparison.OrdinalIgnoreCase) && File.Exists(args[i]))
                return args[i];
        }
        return null;
    }

    private async Task InitializeWebViewAsync()
    {
        var userDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GAManager", "WebView2");

        var environment = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
        await Browser.EnsureCoreWebView2Async(environment);

        // The app has its own context menus (e.g. GaGrid); suppress WebView2's native
        // browser one (Zurück, Neu laden, Element untersuchen, …) everywhere else.
        Browser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

        var wwwroot = Path.Combine(AppContext.BaseDirectory, "wwwroot");
        Browser.CoreWebView2.SetVirtualHostNameToFolderMapping(
            VirtualHostName, wwwroot, CoreWebView2HostResourceAccessKind.Allow);

        Browser.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

        Browser.CoreWebView2.Navigate($"https://{VirtualHostName}/index.html");
    }

    private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        BridgeMessage? msg;
        try
        {
            msg = JsonSerializer.Deserialize<BridgeMessage>(e.WebMessageAsJson, JsonOptions);
        }
        catch (JsonException)
        {
            return;
        }
        if (msg == null) return;

        switch (msg.Type)
        {
            case "ready":
                HandleReady();
                break;
            case "openFile":
                HandleOpenFile(msg.Id);
                break;
            case "openRecent":
                HandleOpenRecent(msg.Id, msg.Path);
                break;
            case "saveFile":
                HandleSave(msg.Id, msg.Content ?? "", forceSaveAs: false);
                break;
            case "saveFileAs":
                HandleSave(msg.Id, msg.Content ?? "", forceSaveAs: true);
                break;
            case "exportFile":
                HandleExport(msg.Id, msg.Content ?? "", msg.FileName ?? "Export.csv");
                break;
            case "importFile":
                HandleImport(msg.Id);
                break;
            case "setDirty":
                _dirty = msg.Dirty ?? false;
                UpdateTitle();
                break;
            case "newProject":
                _currentFile = null;
                _dirty = false;
                UpdateTitle();
                break;
            case "contentResponse":
                if (msg.Id != null && _pendingHostRequests.TryRemove(msg.Id, out var tcs))
                    tcs.SetResult(msg.Content);
                break;
        }
    }

    private void HandleReady()
    {
        SendRecentFiles();

        if (_pendingOpenPath == null) return;

        var path = _pendingOpenPath;
        _pendingOpenPath = null;

        var (ok, content, error) = TryReadProjectFile(path);
        if (ok)
        {
            SendMessage(new { type = "fileOpened", path, content });
        }
        else
        {
            MessageBox.Show(this, $"Datei konnte nicht geöffnet werden:\n{error}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void HandleOpenFile(string? id)
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".gaproj",
            Filter = "GA Project Dateien (*.gaproj)|*.gaproj",
        };

        if (dialog.ShowDialog(this) != true)
        {
            SendMessage(new { type = "openResult", id, ok = false });
            return;
        }

        var (ok, content, error) = TryReadProjectFile(dialog.FileName);
        if (!ok)
        {
            MessageBox.Show(this, $"Datei konnte nicht geöffnet werden:\n{error}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        SendMessage(new { type = "openResult", id, ok, path = ok ? dialog.FileName : null, content, error });
    }

    private void HandleOpenRecent(string? id, string? path)
    {
        if (string.IsNullOrEmpty(path)) return;

        if (!File.Exists(path))
        {
            var stale = _recentFiles.FirstOrDefault(f => f.FullName == path);
            if (stale != null) _recentFiles.Remove(stale);
            WriteRecentFilesList();
            SendRecentFiles();

            MessageBox.Show(this, "Projektdatei nicht gefunden.", new FileInfo(path).Name, MessageBoxButton.OK);
            SendMessage(new { type = "openResult", id, ok = false });
            return;
        }

        var (ok, content, error) = TryReadProjectFile(path);
        if (!ok)
        {
            MessageBox.Show(this, $"Datei konnte nicht geöffnet werden:\n{error}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        SendMessage(new { type = "openResult", id, ok, path = ok ? path : null, content, error });
    }

    private (bool ok, string? content, string? error) TryReadProjectFile(string path)
    {
        try
        {
            var content = File.ReadAllText(path);
            _currentFile = new FileInfo(path);
            _dirty = false;
            UpdateTitle();
            EnqueueRecentFile(path);
            return (true, content, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    private void ReadRecentFilesList()
    {
        if (!File.Exists(_recentFilesPath)) return;

        try
        {
            var json = File.ReadAllText(_recentFilesPath);
            var paths = JsonSerializer.Deserialize<List<string>>(json);
            _recentFiles = paths?.Select(p => new FileInfo(p)).Where(f => f.Exists).Take(10).ToList() ?? [];
        }
        catch (Exception ex) when (ex is IOException or JsonException)
        {
            _recentFiles = [];
        }
    }

    private void WriteRecentFilesList()
    {
        var json = JsonSerializer.Serialize(_recentFiles.Select(f => f.FullName), RecentFilesJsonOptions);
        File.WriteAllText(_recentFilesPath, json);
    }

    private void EnqueueRecentFile(string path)
    {
        var oldEntries = _recentFiles.Where(f => f.FullName != path).Take(9).ToList();
        _recentFiles = [new FileInfo(path), .. oldEntries];

        WriteRecentFilesList();
        SendRecentFiles();
    }

    private void SendRecentFiles()
    {
        SendMessage(new
        {
            type = "recentFiles",
            files = _recentFiles.Select(f => new { path = f.FullName, name = f.Name }),
        });
    }

    private void HandleSave(string? id, string content, bool forceSaveAs)
    {
        var ok = SaveProjectContent(content, forceSaveAs);
        SendMessage(new { type = "saveResult", id, ok, path = _currentFile?.FullName });
    }

    private bool SaveProjectContent(string content, bool forceSaveAs)
    {
        var target = _currentFile;

        if (forceSaveAs || target == null)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = ".gaproj",
                Filter = "GA Project Dateien (*.gaproj)|*.gaproj",
                FileName = target?.Name ?? "Projekt.gaproj",
            };
            if (target?.DirectoryName != null)
                dialog.InitialDirectory = target.DirectoryName;

            if (dialog.ShowDialog(this) != true) return false;
            target = new FileInfo(dialog.FileName);
        }

        try
        {
            File.WriteAllText(target.FullName, content, new UTF8Encoding(false));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Datei konnte nicht gespeichert werden:\n{ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        _currentFile = target;
        _dirty = false;
        UpdateTitle();
        EnqueueRecentFile(target.FullName);
        return true;
    }

    private void HandleExport(string? id, string content, string fileName)
    {
        var dialog = new SaveFileDialog
        {
            DefaultExt = Path.GetExtension(fileName) is { Length: > 0 } ext ? ext : ".csv",
            Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*",
            FileName = fileName,
        };
        if (_currentFile?.DirectoryName != null)
            dialog.InitialDirectory = _currentFile.DirectoryName;

        if (dialog.ShowDialog(this) != true)
        {
            SendMessage(new { type = "exportResult", id, ok = false });
            return;
        }

        try
        {
            File.WriteAllText(dialog.FileName, content, new UTF8Encoding(true));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Datei konnte nicht exportiert werden:\n{ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            SendMessage(new { type = "exportResult", id, ok = false });
            return;
        }

        SendMessage(new { type = "exportResult", id, ok = true, path = dialog.FileName });
    }

    private void HandleImport(string? id)
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".csv",
            Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*",
        };

        if (dialog.ShowDialog(this) != true)
        {
            SendMessage(new { type = "importResult", id, ok = false });
            return;
        }

        try
        {
            var content = ReadTextAutoDetect(dialog.FileName);
            SendMessage(new { type = "importResult", id, ok = true, path = dialog.FileName, content });
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Datei konnte nicht importiert werden:\n{ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            SendMessage(new { type = "importResult", id, ok = false, error = ex.Message });
        }
    }

    /// <summary>
    /// Reads a text file, auto-detecting its encoding: a UTF-8 BOM is honored, otherwise the
    /// bytes are decoded as UTF-8 if valid, and as Windows-1252 otherwise. CSVs exported from
    /// ETS/Excel on German Windows are typically ANSI (Windows-1252) without a BOM, which
    /// File.ReadAllText's UTF-8 default would otherwise mangle for umlauts (ä/ö/ü/ß).
    /// </summary>
    private static string ReadTextAutoDetect(string path)
    {
        var bytes = File.ReadAllBytes(path);

        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            return new UTF8Encoding(false).GetString(bytes, 3, bytes.Length - 3);

        try
        {
            return new UTF8Encoding(false, throwOnInvalidBytes: true).GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            return Encoding.GetEncoding(1252).GetString(bytes);
        }
    }

    private Task<string?> RequestCurrentContentAsync()
    {
        var id = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<string?>();
        _pendingHostRequests[id] = tcs;
        SendMessage(new { type = "requestContent", id });
        return tcs.Task;
    }

    private void SendMessage(object payload)
    {
        if (Browser.CoreWebView2 == null) return;
        Browser.CoreWebView2.PostWebMessageAsJson(JsonSerializer.Serialize(payload, JsonOptions));
    }

    private void UpdateTitle()
    {
        Title = (_currentFile?.Name ?? "Neues Projekt") + (_dirty ? "*" : "");
    }

    private async void Window_Closing(object? sender, CancelEventArgs e)
    {
        if (_forceClose || !_dirty) return;

        e.Cancel = true;

        var result = MessageBox.Show(this, "Änderungen am aktuellen Projekt speichern?", "Projekt geändert",
            MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Cancel) return;

        if (result == MessageBoxResult.Yes)
        {
            var contentTask = RequestCurrentContentAsync();
            var completed = await Task.WhenAny(contentTask, Task.Delay(TimeSpan.FromSeconds(5)));
            if (completed != contentTask)
            {
                MessageBox.Show(this, "Die Anwendung antwortet nicht. Speichern abgebrochen.", "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var content = contentTask.Result;
            if (content == null || !SaveProjectContent(content, forceSaveAs: false)) return;
        }

        // Close() cannot be called reentrantly while this Closing event is still being dispatched
        // (WPF throws "Cannot ... call Close() while a Window is being closed"), so defer it.
        _forceClose = true;
        _ = Dispatcher.BeginInvoke(new Action(Close));
    }
}
