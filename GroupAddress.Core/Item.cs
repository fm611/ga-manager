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
            new GATemplate(SubGroupTemplate.SwitchPair, "Schalten", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sperren", "1", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Sperren","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Szene")
        ];
        public static ItemTemplate LightDimm =
        [
            new GATemplate(SubGroupTemplate.SwitchPair, "Schalten", "", "Status"),

            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmHell", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmHell", "absolut", "Wert"),

            new GATemplate(SubGroupTemplate.MiscPair, "Sperren", "1", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Sperren","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Szene"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","1"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","3"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","4"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_1", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_2", "", "Status")

        ];
        public static ItemTemplate LightTW =
        [
            new GATemplate(SubGroupTemplate.SwitchPair, "Schalten", "", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "HCL", "", "Status"),

            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmHell", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmHell", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmTemp_P", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmTemp_P", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmTemp_K", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmTemp_K", "absolut", "Wert"),

            new GATemplate(SubGroupTemplate.MiscPair, "Sperren", "1", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Sperren","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Szene"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","1"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","3"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","4"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_1", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_2", "", "Status")
        ];
        public static ItemTemplate LightRGBW =
        [
            new GATemplate(SubGroupTemplate.SwitchPair, "Schalten", "", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "HCL", "", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "R", "Schalten", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "G", "Schalten", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "B", "Schalten", "Status"),
            new GATemplate(SubGroupTemplate.SwitchPair, "W", "Schalten", "Status"),


            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_H", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_H", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_S", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_S", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_V", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_V", "absolut", "Wert"),

            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_R", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_R", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_G", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_G", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_B", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_B", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "Dimm_W", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "Dimm_W", "absolut", "Wert"),

            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmTemp_P", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmTemp_P", "absolut", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.GetSpacer, "DimmTemp_K", "relativ"),
            new GATemplate(SubGroupTemplate.ValuePair, "DimmTemp_K", "absolut", "Wert"),

            new GATemplate(SubGroupTemplate.ValuePair, "HSV", "", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, "RGB", "", "Wert"),
            new GATemplate(SubGroupTemplate.ValuePair, GASpacerType.SetSpacer, "RGBW", "Wert"),


            new GATemplate(SubGroupTemplate.MiscPair, "Sperren", "1", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Sperren","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "Szene"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","1"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","2"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","3"),
            new GATemplate(SubGroupTemplate.MiscPair, GASpacerType.GetSpacer, "BitSzene","4"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_1", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_2", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_3", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_4", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_5", "", "Status"),
            new GATemplate(SubGroupTemplate.MiscPair, "Sequenz_6", "", "Status")

        ];

        public Item CreateItem(MainGroup mGroup, string name)
        {
            var item = new Item(name);

            var tempGAs = new List<GA>();

            foreach(var gaTemp in this)
            {
                var grouped = tempGAs.GroupBy(x => x.SubGroup);
                var filtered = grouped.Where(x => 
                    new[] { gaTemp.GetSubGroupTemplate , gaTemp.SetSubGroupTemplate }
                        .Where(s => s != null)
                        .Select(s => mGroup.GetSubGroup(s))
                        .Contains(x.Key)
                );
                var groupCounts = filtered.Select(x => x.Count()); 
                var minGAId = groupCounts.DefaultIfEmpty(0).Max();

                tempGAs.AddRange(gaTemp.CreateGA(mGroup, minGAId, name));
            }

            item.GAs = tempGAs.OrderBy(x => x.SubGroup.MainGroup.Id).ThenBy(x => x.SubGroup.Id).ThenBy(x => x.Id).ToList();
            return item;
        }

    }


    public class Item(string name)
    {
        public string Name { get; set; } = name;

        public List<GA> GAs { get; set; } = [];

        public void ShiftGA(int offset)
        {
            foreach(var ga in GAs)
            {
                ga.Id += offset;
            }
        }
    }


    public class ItemCollection : List<Item>
    {
    }


}
