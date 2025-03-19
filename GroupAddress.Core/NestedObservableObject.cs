using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class NestedObservableObject : ObservableObject
    {

        public bool SetAndSubscribeProperty<T>([NotNullIfNotNull(nameof(newValue))] ref T field, T newValue, [CallerMemberName] string? propertyName = null) where T : INotifyPropertyChanged
        {
            if(field != null)
                field.PropertyChanged -= (sender, e) => OnPropertyChanged(e);
            newValue.PropertyChanged += (sender, e) => OnPropertyChanged(e);

            return SetProperty(ref field, newValue, propertyName);
        }
    }
}
