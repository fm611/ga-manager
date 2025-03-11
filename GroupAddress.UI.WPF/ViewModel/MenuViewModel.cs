using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace GroupAddress.UI.WPF.ViewModel
{
    class MenuViewModel : ObservableObject
    {

        private static string _recentFilesJsonPath = "recent.json";

        private ObservableCollection<FileInfo> _recentFilePaths;
        public ObservableCollection<FileInfo> RecentFilePaths { get => _recentFilePaths; set => SetProperty(ref _recentFilePaths, value); }



        public MenuViewModel()
        {
            ReadRecentFilesList();
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

        private void OpenProjectFile(string path)
        {

        }





    }
}
