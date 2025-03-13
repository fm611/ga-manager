using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupAddress.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GroupAddress.UI.WPF.ViewModel
{
    public partial class  MenuViewModel : ObservableObject
    {

        public event EventHandler<EventArgs>? ProjectRead;

        private static string _recentFilesJsonPath = "recent.json";

        private ObservableCollection<FileInfo> _recentFilePaths = [];
        public ObservableCollection<FileInfo> RecentFilePaths { get => _recentFilePaths; set => SetProperty(ref _recentFilePaths, value); }


        private FileInfo? _currentProjectFile;
        public FileInfo? CurrentProjectFile { get => _currentProjectFile; set => SetProperty(ref _currentProjectFile, value); }

        public ICommand OpenFileCommand { get; set; }
        public ICommand ReadProjectFileCommand { get; set; }

        public MenuViewModel()
        {
            ReadRecentFilesList();
            OpenFileCommand = new RelayCommand(OpenFile);
            ReadProjectFileCommand = new RelayCommand<string>(ReadProjectFile);
        }

        private void OnProjectRead()
        {
            ProjectRead?.Invoke(this, EventArgs.Empty);
        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog();

            dialog.FileName = "project.gaproj"; // Default file name
            dialog.DefaultExt = ".gaproj"; // Default file extension
            dialog.Filter = "GA Project Files (.gaproj)|*.gaproj"; // Filter files by extension

            var res = dialog.ShowDialog();

            if (!res.HasValue || !res.Value) return;


            ReadProjectFile(dialog.FileName);

        }


        private void ReadRecentFilesList()
        {
            if (File.Exists(_recentFilesJsonPath))
            {
                using StreamReader reader = new StreamReader(_recentFilesJsonPath);
                var json = reader.ReadToEnd();
                try
                {
                    var obj = JsonSerializer.Deserialize<List<string>>(json);
                    if (obj == null) throw new Exception();
                    RecentFilePaths = [.. obj.Select(x => new FileInfo(x)).Where(x => x.Exists).Take(10)];
                }
                catch
                {
                    RecentFilePaths = [];
                }
            }
        }

        private void WriteRecentFilesList()
        {
            using StreamWriter outputFile = new StreamWriter(_recentFilesJsonPath, false);
            var json = JsonSerializer.Serialize(RecentFilePaths.Select(x => x.FullName),
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }
            );
            outputFile.Write(json);
        }

        private void ReadProjectFile(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;

            if (!File.Exists(path))
            {
                var fi = RecentFilePaths.FirstOrDefault(x => x.FullName == path);
                if (fi != null)
                    RecentFilePaths.Remove(fi);
                fi = new FileInfo(path);
                WriteRecentFilesList();
                //UpdateRecentFilesMenu();
                MessageBox.Show("Projektdatei nicht gefunden.", fi.Name, MessageBoxButton.OK);
                return;
            }

            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();

            var obj = Project.FromJson(json);

            if (obj != null)
            {
                EnqueueRecentFiles(path);
                OnProjectRead();
                ////CurrentProjectFile = path;
                //SetProjectFile(obj, path);
            }
        }

        private void EnqueueRecentFiles(string path)
        {
            var oldEntries = RecentFilePaths.Where(x => x.FullName != path).Take(9).ToList();
            //RecentFiles.Clear();

            RecentFilePaths = new ObservableCollection<FileInfo>([new FileInfo(path), .. oldEntries]);

            //RecentFiles.Add(new FileInfo(path));
            //RecentFiles.AddRange(oldEntries);

            WriteRecentFilesList();
            //UpdateRecentFilesMenu();
        }





    }
}
