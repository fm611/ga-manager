using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace GroupAddress.UI.WPF.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private Project? project;
        public Project? Project
        {
            get => project; set
            {
                project = value;
                OnPropertyChanged();
            }
        }


        private MenuViewModel _menuViewModel;
        public MenuViewModel MenuViewModel { get => _menuViewModel; set => SetProperty(ref _menuViewModel, value); }


        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
        }



    }
}
