using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class Item
    {
        private string name = "";

        public event EventHandler<EventArgs> Changed;
        public string Id { get; set; }
        public string Name { get => name; set
            {
                name = value;
                ItemChanged();
            }
        }
        private Item()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void ItemChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public Item(string name) : this() { 
            Name = name;
        }
    }
}
