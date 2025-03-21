using CommunityToolkit.Mvvm.ComponentModel;
using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI.WPF.ViewModel
{
    public class ProjectViewModel : ObservableObject
    {
        private Project _project;
        public Project Project { get => _project; set => SetProperty(ref _project, value); }

        private FileInfo? _currentProjectFile;
        public FileInfo? CurrentProjectFile { get => _currentProjectFile; set => SetProperty(ref _currentProjectFile, value); }

        public ProjectViewModel()
        {
            Project = new Project();
        }
    }
}
