using System.Data.SqlTypes;

namespace GroupAddress.Core
{
    public abstract class Group
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public Group() { }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class MainGroup : Group
    {
        public List<SubGroup> SubGroups { get; set; } = [];

        private ItemCollection _items;

        public bool FillGASpaces { get; set; }
        public bool FillGAToEnd { get; set; }

        public int NextItemId { get; private set; } = 0;

        public MainGroup(int id, string name) : base(id, name)
        {
            _items = new ItemCollection();
        }

        public void AddItem(ItemTemplate template, string name, int blockLength=0)
        {
            var newItem = template.CreateItem(this, name);
            newItem.ShiftGA(NextItemId);

            var maxId = newItem.GAs.Select(x => x.Id).Max();
            var newItemBlockLength = blockLength != 0 ? blockLength : maxId+1;
            NextItemId += newItemBlockLength;

            _items.Add(newItem);
        }

        public List<GA> GetAllGAs() 
        {
            var itemGAs = _items.SelectMany(x => x.GAs);
            var subGroups = itemGAs.GroupBy(x => x.SubGroup);

            var outputGAs = new List<GA>(itemGAs);

            if (FillGASpaces)
            {
                var fillBlockerGAs = new List<GA>();
                foreach (var group in subGroups)
                {
                    var maxGroupId = NextItemId;
                    var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.Id != x));
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
                    var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.Id != x));
                    fillToEndBlockerGAs.AddRange(freeIds.Select(id => new GA(group.Key, id, "Blocker")));
                }
                outputGAs.AddRange(fillToEndBlockerGAs);
            }

            return [.. outputGAs.OrderBy(x => x.SubGroup.MainGroup.Id).ThenBy(x => x.SubGroup.Id).ThenBy(x => x.Id)];

        }

        public SubGroup GetOrCreateSubGroup(SubGroupTemplate subGroupTemplate)
        {
            var res = GetSubGroup(subGroupTemplate);

            return res ?? SubGroup.Create(subGroupTemplate, this);

        }

        public SubGroup? GetSubGroup(SubGroupTemplate subGroupTemplate)
        {
            return SubGroups.Where(x => x.Id == subGroupTemplate.SubAddress).FirstOrDefault();
        }

        public string GetCSVString()
        {
            var gas = GetAllGAs();

            var grouped = gas.GroupBy(x => x.SubGroup);

            var outputStr = string.Join(";", new string[] { Name, "", "", Id.ToString(), "", "" })+"\n";

            foreach (var subGroup in grouped)
            {
                var ordered = subGroup.OrderBy(x => x.Id).ToList();

                outputStr += string.Join(";", new string[] { "", subGroup.Key.Name, "", Id.ToString(), subGroup.Key.Id.ToString(), "" }) + "\n";
                outputStr += string.Join("\n",ordered.Select(g => string.Join(";", new string[] { "", "", g.Name, Id.ToString(), subGroup.Key.Id.ToString(), g.Id.ToString() })));
                outputStr += "\n";
            }
            return outputStr;            
        }
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

        public string Address
        {
            get => MainGroup.Id + "/" + Id;
            set => _ = value;
        }

        public SubGroup() { }

        public SubGroup(int id, string name, MainGroup mainGroup) : base(id, name)
        {
            MainGroup = mainGroup;
            mainGroup.SubGroups.Add(this);
        }
                

        public override string ToString()
        {
            return Address;
        }

        public static SubGroup Create(SubGroupTemplate t, MainGroup mGroup)
        {
            return new SubGroup(t.SubAddress, t.Name, mGroup);
        }
    }
}
