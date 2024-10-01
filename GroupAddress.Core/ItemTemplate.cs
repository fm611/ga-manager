using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class ItemTemplate
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public List<ItemPartTemplate> PartTemplates { get; set; } = [];

        public ItemTemplate()
        {
            Id = Guid.NewGuid().ToString();
            Name = "";
        }

        public ItemTemplate(string name) : this()
        {
            Name = name;
        }

        public ItemTemplate(string name, IEnumerable<ItemPartTemplate> partTemplates) : this(name)
        {
            PartTemplates = partTemplates.ToList();
            foreach (ItemPartTemplate partTemplate in PartTemplates)
            {
                partTemplate.ItemTemplate = this;
            }
        }


        public Item CreateItem(List<MainGroup> mGroups, string itemName)
        {
            var item = new Item(itemName);

            if (mGroups.Count != PartTemplates.Count) throw new Exception("Invalid number of MainGroups");

            var tempParts =new List<ItemPart>();


            for (int i = 0; i < PartTemplates.Count; i++)
            {
                var partTemp = PartTemplates[i];
                var mGroup = mGroups[i];

                

                var itemPart = mGroup.AddItemPart(partTemp, itemName);
                itemPart.Item = item;


                tempParts.Add(itemPart);

            }

            item.Parts = tempParts.OrderBy(x => x.MainGroup.SubAddress).ToList();

            return item;
        }



    }

    // Bspw. RGBW Vorlage
    public class ItemPartTemplate
    {
        public string Name { get; set; } = "";
        public string Id { get; set; }

        public List<GATemplate> GATemplates { get; set; } = [];

        public ItemTemplate? ItemTemplate { get; set; }

        public ItemPartTemplate()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ItemPartTemplate(string name) : this()
        {
            Name = name;
        }

        public ItemPartTemplate(string name, IEnumerable<GATemplate> gatemplates) : this(name)
        {
            GATemplates = gatemplates.ToList();
        }


        public ItemPart CreateItemPart(MainGroup mGroup, string gaPrefix)
        {
            var itemPart = new ItemPart(Name,mGroup);

            var tempGAs = new List<GA>();

            foreach (var gaTemp in GATemplates)
            {
                //var grouped = tempGAs.GroupBy(x => x.SubGroup);
                //var filtered = grouped.Where(x =>
                //    gaTemp.GAParts.Select(p => p.subGroupTemplate)
                //        .Where(s => s != null)
                //        .Select(s => mGroup.GetSubGroup(s))
                //        .Contains(x.Key)
                //);
                var grouped = tempGAs.GroupBy(x => x.SubGroup.SubAddress);
                var filtered = grouped.Where(x =>
                    gaTemp.GAParts.Select(p => p.subGroupTemplate.SubAddress)
                        .Contains(x.Key)
                );
                var ids = filtered.SelectMany(x => x.Select(y => y.SubAddress));
                var minGAId = !ids.Any() ? 0 : ids.Max() + 1;

                tempGAs.AddRange(gaTemp.CreateGA(mGroup, minGAId, gaPrefix));
            }

            itemPart.GAs = tempGAs.OrderBy(x => x.SubGroup.MainGroup.SubAddress).ThenBy(x => x.SubGroup.SubAddress).ThenBy(x => x.SubAddress).ToList();
            return itemPart;
        }

    }
}
