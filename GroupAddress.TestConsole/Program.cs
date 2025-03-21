using GroupAddress.Core;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GroupAddress.TestConsole
{
    internal class Program
    {


        static void Main(string[] args)
        {

            var coll = new ObservableCollection<Bar>();

            coll.CollectionChanged += (sender, e) => Debug.WriteLine("Collection changed");

            var b = new Bar() { Content = "1234" };

            coll.Add(b);

            b.Content = "56678";


        }
    }



    public class Bar : ObservableObject
    {
        private string _content;
        public string Content { get => _content; set => SetProperty(ref _content, value); }


    }

}

