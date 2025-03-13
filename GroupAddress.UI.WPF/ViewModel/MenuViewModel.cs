using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupAddress.Core;
using GroupAddress.UI.WPF.CustomEventArgs;
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


        public event EventHandler<ProjectOpenEventArgs>? ProjectOpen;

        private static string _recentFilesJsonPath = "recent.json";

        private ObservableCollection<FileInfo> _recentFilePaths = [];
        public ObservableCollection<FileInfo> RecentFilePaths { get => _recentFilePaths; set => SetProperty(ref _recentFilePaths, value); }


        private FileInfo? _currentProjectFile;
        public FileInfo? CurrentProjectFile { get => _currentProjectFile; set => SetProperty(ref _currentProjectFile, value); }

        public ICommand OpenFileCommand { get; set; }
        public ICommand ReadProjectFileCommand { get; set; }
        public ICommand OpenSampleProjectCommand { get; set; }


        public Project Project { get; set; }

        public MenuViewModel()
        {
            ReadRecentFilesList();
            OpenFileCommand = new RelayCommand(OpenFile);
            ReadProjectFileCommand = new RelayCommand<string>(ReadProjectFile);
            OpenSampleProjectCommand = new RelayCommand(OpenSampleProject);

        }

        private void OnProjectRead(Project project, FileInfo? fileInfo = null)
        {
            ProjectOpen?.Invoke(this, new ProjectOpenEventArgs(project, fileInfo));
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
                EnqueueRecentFile(path);   
                //CurrentProjectFile = path;
                SetProject(obj, new FileInfo(path));
            }
        }

        private void OpenSampleProject()
        {

        }

        private bool HandleProjectChanged()
        {
            if (!ProjectDirty) return true;

            var res2 = MessageBox.Show("Änderungen am aktuellen Projekt speichern?", "Projekt geändert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res2 == DialogResult.Cancel) return false;
            if (res2 == DialogResult.Yes) return Save();
            return true;
        }


        private void SetProject(Project project, FileInfo fileInfo)
        {
            Project = project;
            OnProjectRead(project, fileInfo);

        }


        private void EnqueueRecentFile(string path)
        {
            var oldEntries = RecentFilePaths.Where(x => x.FullName != path).Take(9).ToList();
            RecentFilePaths = new ObservableCollection<FileInfo>([new FileInfo(path), .. oldEntries]);
            WriteRecentFilesList();
        }





    }
}
