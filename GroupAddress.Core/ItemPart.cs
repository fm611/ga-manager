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
        public string Name { get; set; }
        public List<ItemPart> Parts { get; set; }

        public Item() => Id = Guid.NewGuid().ToString();

        public Item(string name) : this()
        {
            Name = name;
        }

    }

    public class ItemPart
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Item Item { get; set; }

        public List<GA> GAs { get; set; } = [];

        public MainGroup MainGroup { get; set; }

        public ItemPart() => Id = Guid.NewGuid().ToString();

        public ItemPart(string name,MainGroup mGroup) : this()
        {
            Name = name;
            MainGroup = mGroup;
        }

        public void ShiftGA(int offset)
        {
            foreach(var ga in GAs)
            {
                ga.SubAddress += offset;
            }
        }
    }



}
