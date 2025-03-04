using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI.WPF.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        private string test;

        public string Test { get => test; set { 
                test = value;
                NotifyPropertyChanged(nameof(Test));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


    }
}
