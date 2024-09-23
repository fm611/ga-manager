using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Item ParentItem { get; set; }

        public List<Item> ChildItems { get; set; } = [];

        public List<TemplateGA> TemplateGAs { get; set; }

        public Item()
        {
            TemplateGAs = new List<TemplateGA>();
        }

        public Item(Item parentItem) : this()
        {
            ParentItem = parentItem;
            ParentItem.ChildItems.Add(this);

        }
    }
}
