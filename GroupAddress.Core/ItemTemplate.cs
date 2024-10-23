using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class ItemTemplate : TopLevelCollection
    {

        private ItemTemplate() : base() { }

        public ItemTemplate(string name, IEnumerable<GA> gas) : base(name, gas)
        {
        }


    }



    // Bspw. RGBW Vorlage
    // je Hauptgruppe
    //public class ItemTemplate
    //{
    //    public string Name { get; set; } = "";
    //    public string Id { get; set; }

    //    public List<GATemplate> GATemplates { get; set; } = [];

    //    public ItemTemplate()
    //    {
    //        Id = Guid.NewGuid().ToString();
    //    }
    //    public ItemTemplate(string name) : this()
    //    {
    //        Name = name;
    //    }
    //    public ItemTemplate(string name, IEnumerable<GATemplate> gatemplates) : this(name)
    //    {
    //        gatemplates.ToList().ForEach(x => AddGaTemplate(x));
    //    }


    //    public void AddGaTemplate(GATemplate gat)
    //    {
    //        var maxIds = GATemplates
    //            .SelectMany(temp => temp.GAParts.Select(gap => new { SubAddress = temp.SubAddress, SubGroup = gap.SubGroupTemplate.SubAddress }))
    //            .GroupBy(x => x.SubGroup)
    //            .Select(x => new { SubGroup = x.Key, MaxId = x.Max(y => y.SubAddress) });

    //        var maxId = maxIds
    //            .Where(x => gat.GAParts.Select(y => y.SubGroupTemplate.SubAddress).Contains(x.SubGroup))
    //            .Select(x => x.MaxId)
    //            .DefaultIfEmpty(-1)
    //            .Max();

    //        gat.SubAddress = maxId+1;    
    //        GATemplates.Add(gat);
    //    }



    //    public Item CreateItem(MainGroup mGroup, string gaPrefix)
    //    {
    //        var item = new Item(Name,mGroup);

    //        var tempGAs = GATemplates.SelectMany(x => x.CreateGA(mGroup, gaPrefix));

    //        item.GAs = [.. GATemplates
    //            .SelectMany(x => x.CreateGA(mGroup, gaPrefix))
    //            .OrderBy(x => x.SubGroup.MainGroup.SubAddress)
    //            .ThenBy(x => x.SubGroup.SubAddress)
    //            .ThenBy(x => x.SubAddress)];
    //        return item;
    //    }

    //}

}
