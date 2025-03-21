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
    public partial class MenuViewModel : ObservableObject
    {


        private static string _recentFilesJsonPath = "recent.json";

        private ObservableCollection<FileInfo> _recentFilePaths = [];
        public ObservableCollection<FileInfo> RecentFilePaths { get => _recentFilePaths; set => SetProperty(ref _recentFilePaths, value); }

        public ICommand OpenFileCommand { get; set; }
        public ICommand ReadProjectFileCommand { get; set; }
        public ICommand OpenSampleProjectCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }

        public ProjectViewModel ProjectViewModel { get; set; }

        public Project Project { get => ProjectViewModel.Project; set => ProjectViewModel.Project = value; }
        public FileInfo? CurrentProjectFile { get => ProjectViewModel.CurrentProjectFile; set => ProjectViewModel.CurrentProjectFile = value; }

        public MenuViewModel(ProjectViewModel projectViewModel)
        {
            ProjectViewModel = projectViewModel;

            ReadRecentFilesList();
            OpenFileCommand = new RelayCommand(OpenFile);
            ReadProjectFileCommand = new RelayCommand<string>(ReadProjectFile);
            OpenSampleProjectCommand = new RelayCommand(OpenSampleProject);
            NewCommand = new RelayCommand(NewProject);
            SaveCommand = new RelayCommand(() => Save());
            SaveAsCommand = new RelayCommand(() => SaveAs());
        }

        private bool HandleProjectChanged()
        {
            if (!Project.Dirty) return true;

            var res2 = MessageBox.Show("Änderungen am aktuellen Projekt speichern?", "Projekt geändert", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (res2 == MessageBoxResult.Cancel) return false;
            if (res2 == MessageBoxResult.Yes) return Save();
            return true;
        }

        private void NewProject()
        {
            if (!HandleProjectChanged()) return;

            SetProject(new Project(), null);
        }

        private void OpenFile()
        {
            if (!HandleProjectChanged()) return;
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

        private void EnqueueRecentFile(string path)
        {
            var oldEntries = RecentFilePaths.Where(x => x.FullName != path).Take(9).ToList();
            RecentFilePaths = new ObservableCollection<FileInfo>([new FileInfo(path), .. oldEntries]);
            WriteRecentFilesList();
        }

        private void ReadProjectFile(string? path)
        {
            if (!HandleProjectChanged()) return;
            if (string.IsNullOrEmpty(path)) return;

            if (!File.Exists(path))
            {
                var fi = RecentFilePaths.FirstOrDefault(x => x.FullName == path);
                if (fi != null)
                    RecentFilePaths.Remove(fi);
                fi = new FileInfo(path);
                WriteRecentFilesList();
                MessageBox.Show("Projektdatei nicht gefunden.", fi.Name, MessageBoxButton.OK);
                return;
            }

            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();

            var obj = Project.FromJson(json);

            if (obj != null)
            {
                EnqueueRecentFile(path);
                SetProject(obj, new FileInfo(path));
            }
        }

        private void OpenSampleProject()
        {
            if (!HandleProjectChanged()) return;

            SetProject(Project.GetSampleProject(), null);
        }

        public void SetProject(Project project, FileInfo? fileInfo)
        {
            Project = project;
            CurrentProjectFile = fileInfo;
        }

        private bool SaveAs()
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "gaproj";
            saveDialog.Filter = "Project files (*.gaproj)|*.gaproj";

            var res = saveDialog.ShowDialog();


            if (!res.HasValue || res == false) return false;

            var filePath = saveDialog.FileName;
            CurrentProjectFile = new FileInfo(filePath);

            return Save();
        }

        private bool Save()
        {
            if (CurrentProjectFile == null)
                return SaveAs();

            using StreamWriter outputFile = new StreamWriter(CurrentProjectFile.FullName, false);
            var json = Project.GetJson();
            outputFile.Write(Project.GetJson());

            EnqueueRecentFile(CurrentProjectFile.FullName);
            Project.SetUndirty();

            return true;

        }






    }
}
