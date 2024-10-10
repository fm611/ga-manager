using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    // Mehrere Hauptgruppen

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
    // je Hauptgruppe
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
            gatemplates.ToList().ForEach(x => AddGaTemplate(x));
        }


        public void AddGaTemplate(GATemplate gat)
        {
            var maxIds = GATemplates
                .SelectMany(temp => temp.GAParts.Select(gap => new { SubAddress = temp.SubAddress, SubGroup = gap.SubGroupTemplate.SubAddress }))
                .GroupBy(x => x.SubGroup)
                .Select(x => new { SubGroup = x.Key, MaxId = x.Max(y => y.SubAddress) });

            var maxId = maxIds
                .Where(x => gat.GAParts.Select(y => y.SubGroupTemplate.SubAddress).Contains(x.SubGroup))
                .Select(x => x.MaxId)
                .DefaultIfEmpty(-1)
                .Max();

            gat.SubAddress = maxId+1;    
            GATemplates.Add(gat);
        }


        public ItemPart CreateItemPart(MainGroup mGroup, string gaPrefix)
        {
            var itemPart = new ItemPart(Name,mGroup);

            var tempGAs = GATemplates.SelectMany(x => x.CreateGA(mGroup, gaPrefix));

            itemPart.GAs = [.. GATemplates
                .SelectMany(x => x.CreateGA(mGroup, gaPrefix))
                .OrderBy(x => x.SubGroup.MainGroup.SubAddress)
                .ThenBy(x => x.SubGroup.SubAddress)
                .ThenBy(x => x.SubAddress)];
            return itemPart;
        }

    }
}
