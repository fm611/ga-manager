using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class DeepChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref='System.ComponentModel.PropertyChangedEventArgs'/>
        /// class.
        /// </summary>
        public DeepChangeEventArgs(string? callerName)
        {
            CallerName = callerName;
        }

        /// <summary>
        /// Indicates the name of the property that changed.
        /// </summary>
        public virtual string? CallerName { get; }
    }

    public delegate void DeepChangeEventHandler(object? sender, DeepChangeEventArgs e);


    public interface INotifyDeepChange
    {
        event DeepChangeEventHandler? DeepChange;
    }


}
