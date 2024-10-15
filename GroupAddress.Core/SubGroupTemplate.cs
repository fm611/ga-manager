using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class SubGroupTemplate
    {
        public static SubGroupTemplate Switch = new SubGroupTemplate(1, "Switch");
        public static SubGroupTemplate SwitchStatus = new SubGroupTemplate(2, "Switch Status");
        public static SubGroupTemplate SetValue = new SubGroupTemplate(3, "Set Value");
        public static SubGroupTemplate GetValue = new SubGroupTemplate(4, "Get Value");
        public static SubGroupTemplate SetMisc = new SubGroupTemplate(5, "Set Misc");
        public static SubGroupTemplate GetMisc = new SubGroupTemplate(6, "Get Misc");

        public static SubGroupTemplatePair SwitchPair = new SubGroupTemplatePair(Switch, SwitchStatus);
        public static SubGroupTemplatePair ValuePair = new SubGroupTemplatePair(SetValue, GetValue);
        public static SubGroupTemplatePair MiscPair = new SubGroupTemplatePair(SetMisc, GetMisc);


        public static List<SubGroupTemplate> DefaultLightTemplates = [Switch, SwitchStatus, SetValue, GetValue, SetMisc, GetMisc];

        public string Id { get; set; }
        public string Name { get; set; }
        public int SubAddress { get; set; }

        public SubGroupTemplate() => Id = Guid.NewGuid().ToString();


        public SubGroupTemplate(int address, string name) : this()
        {
            SubAddress = address;
            Name = name;
        }
    }

    public class SubGroupTemplatePair(SubGroupTemplate setGroup, SubGroupTemplate getGroup)
    {
        public SubGroupTemplate GetGroup { get; set; } = getGroup;
        public SubGroupTemplate SetGroup { get; set; } = setGroup;

    }
}
