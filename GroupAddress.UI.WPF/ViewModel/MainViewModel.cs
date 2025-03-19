using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupAddress.Core;
using GroupAddress.UI.WPF.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MenuViewModel MenuViewModel { get; set; }
        public ProjectViewModel ProjectViewModel { get; set; }


        private string _title;
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public ICommand LoadedCommand { get; set; }
        public ICommand TestCommand { get; set; }



        public MainViewModel()
        {
            ProjectViewModel = new ProjectViewModel();
            ProjectViewModel.PropertyChanged += ProjectViewModel_PropertyChanged;

            MenuViewModel = new MenuViewModel(ProjectViewModel);
            LoadedCommand = new RelayCommand(Loaded);
            TestCommand = new RelayCommand(Test);

            SetTitle();
        }

        private void Loaded()
        {
            ProjectViewModel.Project = new Project();
        }

        private void ProjectViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetTitle();
        }

        private void MenuViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void Test()
        {
            ProjectViewModel.Project.AddMainGroup(new MainGroup(44, "Testgruppe", MainGroup.DefaultSubGroupNames));


        }

        private void SetTitle()
        {
            if (MenuViewModel.CurrentProjectFile == null)
                Title = "Neues Projekt";            
            else            
                Title = MenuViewModel.CurrentProjectFile.Name;
            
            if (ProjectViewModel.Project?.Dirty ?? false) Title += "*";
        }

    }
}
