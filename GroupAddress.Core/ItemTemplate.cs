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

        public Dictionary<int, string> SubGroupTemplatesDict => GATemplates
            .SelectMany(x => x.GAParts)
            .Select(x => x.subGroupTemplate).Select(x => new { SubAddress = x.SubAddress, Name = x.Name })
            .GroupBy(x => x).Select(x => x.Key).ToDictionary(x => x.SubAddress, x => x.Name);






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

            //GATemplates = gatemplates.ToList();

            //GenerateSubAddresses();
        }


        public void AddGaTemplate(GATemplate gat)
        {

            var currGas = GATemplates
                .Select(x => new { GaAddress = x.SubAddress, SubGroups = x.GAParts.Select(x => x.subGroupTemplate.SubAddress) });
            var filtered = currGas.Where(x => x.SubGroups.SequenceEqual(gat.GAParts.Select(x => x.subGroupTemplate.SubAddress)));
            var maxId = filtered.Select(x => x.GaAddress).DefaultIfEmpty(-1).Max();

            gat.SubAddress = maxId+1;    
            GATemplates.Add(gat);
        }

        public void GenerateSubAddresses()
        {
            var subGroupMaxIdDict = GATemplates
                .SelectMany(x => x.GAParts)
                .GroupBy(gap => gap.subGroupTemplate.SubAddress)
                .ToDictionary(x => x.Key, x => -1);

            foreach(var gaTemplate in  GATemplates)
            {
                var subs = gaTemplate.GAParts.Select(x => x.subGroupTemplate.SubAddress);

                var c = subGroupMaxIdDict
                    .Where(x => subs.Contains(x.Key))
                    .Max(x => x.Value)+1;

                gaTemplate.SubAddress = c;

                foreach(var sub in subs)
                {
                    subGroupMaxIdDict[sub] = c;
                }
            }

        }



        public ItemPart CreateItemPart(MainGroup mGroup, string gaPrefix)
        {
            var itemPart = new ItemPart(Name,mGroup);

            var tempGAs = new List<GA>();

            foreach (var gaTemp in GATemplates)
            {
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
