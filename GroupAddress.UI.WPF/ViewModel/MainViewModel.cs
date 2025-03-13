using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupAddress.Core;
using GroupAddress.UI.WPF.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;

namespace GroupAddress.UI.WPF.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private Project? _project;
        public Project? Project { get => _project; set => SetProperty(ref _project, value); }

        private string _title;
        public string Title { get => _title; set => SetProperty(ref _title, value); }



        private MenuViewModel _menuViewModel;
        public MenuViewModel MenuViewModel { get => _menuViewModel; set => SetProperty(ref _menuViewModel, value); }


        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            Project = MenuViewModel.Project;
            SetTitle();
            MenuViewModel.ProjectOpen += (sender, e) => OnProjectOpen(sender,e);
        }

        private void OnProjectOpen(object? sender, ProjectOpenEventArgs e)
        {
            Project = e.Project;
            SetTitle();
        }

        private void SetTitle()
        {
            if (MenuViewModel.CurrentProjectFile == null)
                Title = "Neues Projekt";            
            else            
                Title = MenuViewModel.CurrentProjectFile.Name;
            
            if (Project?.Dirty ?? false) Title += "*";
        }

    }
}
