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
        public string Id { get; set; }
        public string Name { get; set; } = "";

        private Item()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Item(string name) : this() { 
            Name = name;
        }


    }



}
