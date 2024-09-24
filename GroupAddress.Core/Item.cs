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

        public List<GATemplateGroup> GATemplateGroups { get; set; }

        public Item()
        {
            GATemplateGroups = [];
        }

        public Item(Item parentItem) : this()
        {
            ParentItem = parentItem;
            ParentItem.ChildItems.Add(this);

        }

        public List<GA> CreateGA(string preString)
        {
            var outputGAs = new List<GA>();

            foreach(var tempGroup in GATemplateGroups)
            {


                var minGaId = outputGAs.GroupBy(x => x.MiddleGroup)
                    .Where(x => tempGroup.Select(tg => tg.MiddleGroup)
                        .Contains(x.Key))
                    .Select(x => x.Count())
                    .Max();

                foreach (var tempGA in tempGroup)
                {
                    outputGAs.Add(tempGA.CreateGA(minGaId, preString));
                }
            }      

            return outputGAs;
        }
    }
}
