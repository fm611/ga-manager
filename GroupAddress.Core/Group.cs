using System.Data.SqlTypes;

namespace GroupAddress.Core
{
    public abstract class Group
    {

        public string Id { get; set; }
        public int SubAddress { get; set; }

        public string Name { get; set; }

        public Group() {
            Id = Guid.NewGuid().ToString();
        }

        public Group(int subAddress, string name) : this()
        {
            SubAddress = subAddress;
            Name = name;
        }

        public abstract string Address { get; }

        public string AddressName => Address + " - " + Name;

        public override string ToString()
        {
            return Address + " - " + Name;
        }
    }

    public class MainGroup : Group
    {
        public List<SubGroup> SubGroups { get; set; } = [];

        public List<Item> Items { get; set; } = [];

        public List<GA> GAs => Items.SelectMany(x => x.GAs).ToList();

        public int MaxGASubAddress => GAs.Select(x => x.SubAddress).DefaultIfEmpty(0).Max();

        public bool FillGASpaces { get; set; }
        public bool FillGAToEnd { get; set; }

        public int NextItemId { get; private set; } = 0;

        public MainGroup() : base() { }
        public MainGroup(int id, string name) : base(id, name)
        {

        }

        public Item AddItem(ItemTemplate template, string gaPrefix, int blockLength=0)
        {
            var newItemPart = template.CreateItemPart(this, gaPrefix);
            newItemPart.ShiftGA(NextItemId);

            var maxId = newItemPart.GAs.Select(x => x.SubAddress).Max();
            var newItemBlockLength = blockLength != 0 ? blockLength : maxId+1;
            NextItemId += newItemBlockLength;

            Items.Add(newItemPart);
            return newItemPart;
        }



        public List<GA> GetAllGAs() 
        {
            var itemGAs = Items.SelectMany(x => x.GAs);
            var subGroups = itemGAs.GroupBy(x => x.SubGroup);

            var outputGAs = new List<GA>(itemGAs);

            if (FillGASpaces)
            {
                var fillBlockerGAs = new List<GA>();
                foreach (var group in subGroups)
                {
                    var maxGroupId = NextItemId;
                    var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.SubAddress != x));
                    fillBlockerGAs.AddRange(freeIds.Select(id => new GA(group.Key, id, "Blocker")));
                }
                outputGAs.AddRange(fillBlockerGAs);
            }

            if (FillGAToEnd)
            {
                var fillToEndBlockerGAs = new List<GA>();
                foreach (var group in subGroups)
                {
                    var maxGroupId = 256;
                    var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.SubAddress != x));
                    fillToEndBlockerGAs.AddRange(freeIds.Select(id => new GA(group.Key, id, "Blocker")));
                }
                outputGAs.AddRange(fillToEndBlockerGAs);
            }

            return [.. outputGAs.OrderBy(x => x.SubGroup.MainGroup.SubAddress).ThenBy(x => x.SubGroup.SubAddress).ThenBy(x => x.SubAddress)];

        }

        public SubGroup GetOrCreateSubGroup(SubGroupTemplate subGroupTemplate)
        {
            var res = GetSubGroup(subGroupTemplate);

            return res ?? SubGroup.Create(subGroupTemplate, this);

        }
        public SubGroup? GetSubGroup(SubGroupTemplate subGroupTemplate)
        {
            return SubGroups.Where(x => x.SubAddress == subGroupTemplate.SubAddress).FirstOrDefault();
        }

        public string GetCSVString()
        {
            var gas = GetAllGAs();

            var grouped = gas.GroupBy(x => x.SubGroup);

            var outputStr = string.Join(";", new string[] { Name, "", "", SubAddress.ToString(), "", "" })+"\n";

            foreach (var subGroup in grouped)
            {
                var ordered = subGroup.OrderBy(x => x.SubAddress).ToList();

                outputStr += string.Join(";", new string[] { "", subGroup.Key.Name, "", SubAddress.ToString(), subGroup.Key.SubAddress.ToString(), "" }) + "\n";
                outputStr += string.Join("\n",ordered.Select(g => string.Join(";", new string[] { "", "", g.Name, SubAddress.ToString(), subGroup.Key.SubAddress.ToString(), g.SubAddress.ToString() })));
                outputStr += "\n";
            }
            return outputStr;            
        }

        public override string Address  => SubAddress.ToString(); 

        public int ItemCount()
        {
            return Items.Count;
        }

        public string AddressNameMaxGA => AddressName + " (" + (MaxGASubAddress == 0 ? "-" : MaxGASubAddress-1) + ")";

        
    }

    public class SubGroupTemplatePair(SubGroupTemplate setGroup, SubGroupTemplate getGroup)
    {
        public SubGroupTemplate GetGroup { get; set; } = getGroup;
        public SubGroupTemplate SetGroup { get; set; } = setGroup;

    }

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


    public class SubGroup : Group
    {
        public MainGroup MainGroup { get; set; }

        public List<GA> GAs { get; set; } = [];

        public override string Address => MainGroup.SubAddress + "/" + SubAddress;

        public SubGroup() :base() { }

        public SubGroup(int id, string name, MainGroup mainGroup) : base(id, name)
        {
            MainGroup = mainGroup;
            mainGroup.SubGroups.Add(this);
        }
                

        public override string ToString()
        {
            return Address + " - " + Name;
        }

        public static SubGroup Create(SubGroupTemplate t, MainGroup mGroup)
        {
            return new SubGroup(t.SubAddress, t.Name, mGroup);
        }
    }
}
