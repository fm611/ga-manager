using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    // Bspw. RGBW Vorlage
    public class ItemTemplate : List<GATemplate>
    {
        public static ItemTemplate Light =
        [
            new GATemplate(SubGroupTemplate.Switch, SubGroupTemplate.SwitchStatus, "Schalten", "", "Status"),
            new GATemplate(SubGroupTemplate.SetMisc, SubGroupTemplate.GetMisc, "Sperren", "1", "Status"),
            new SetGATemplate(SubGroupTemplate.SetMisc, "Sperren","2")
        ];
        public static ItemTemplate LightDimm =
        [
        ];
        public static ItemTemplate LightTW =
        [
        ];
        public static ItemTemplate LightRGBW =
        [
            new GetGATemplate(SubGroupTemplate.GetValue, "RGBW")
        ];

        public Item GetItem(MainGroup mGroup, string name)
        {
            var item = new Item();
            item.Name = name;

            var gas = new List<GA>();

            foreach(var gaTemp in this)
            {
                var grouped = gas.GroupBy(x => x.MiddleGroup);
                var filtered = grouped.Where(x => 
                    mGroup.GetSubGroup(gaTemp.GetSubGroupTemplate) == x.Key ||
                    mGroup.GetSubGroup(gaTemp.SetSubGroupTemplate) == x.Key);
                var groupCounts = filtered.Select(x => x.Count()); 
                var minGAId = groupCounts.DefaultIfEmpty(0).Max();

                gas.AddRange(gaTemp.CreateGA(mGroup, minGAId, name));

            }

            return item;
        }
    }




    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<GA> GATemplateGroups { get; set; }

        public Item()
        {
            GATemplateGroups = [];
        }


        //public List<GA> CreateGA(string preString)
        //{
        //    var outputGAs = new List<GA>();
        //    if(ParentItem != null)
        //        outputGAs.AddRange(ParentItem.CreateGA(preString));

        //    foreach(var tempGroup in GATemplateGroups)
        //    {

        //        var outputGroups = outputGAs.GroupBy(x => x.MiddleGroup);
        //        var outputGroupsFiltered = outputGroups.Where(x => tempGroup.Select(tg => tg.MiddleGroup).Contains(x.Key));
        //        var groupCounts = outputGroupsFiltered.Select(x => x.Count());
        //        var minGAId = groupCounts.DefaultIfEmpty(0).Max();

        //        foreach (var tempGA in tempGroup)
        //        {
        //            outputGAs.Add(tempGA.CreateGA(minGAId, preString));
        //        }
        //    }      

        //    return outputGAs;
        //}
    }
}
