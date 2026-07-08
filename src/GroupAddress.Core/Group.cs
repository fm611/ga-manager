using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class Group
    {
        private string name = "";

        public event EventHandler<EventArgs> Changed;
        public string Id { get; set; }
        public string Name { get => name; set
            {
                name = value;
                OnChange();
            }
        }
        private Group()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void OnChange()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public Group(string name) : this() { 
            Name = name;
        }
    }
}
