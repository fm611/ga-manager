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
        private int defaultBlockLength = 1;

        public List<SubGroup> SubGroups { get; set; } = [];

        public List<Item> Items { get; set; } = [];

        public List<GA> GAs { get; set; } = [];

        public int MaxGASubAddress => GAs.Select(x => x.SubAddress).DefaultIfEmpty(-1).Max();


        public int DefaultBlockLength { get => defaultBlockLength; set
            {
                defaultBlockLength = value;
            }
        }


        public MainGroup() : base() 
        { 
        }
        public MainGroup(int id, string name, int defaultBlockLength = 1) : base(id, name)
        {
            DefaultBlockLength = defaultBlockLength;
        }


        public Item AddItem(ItemTemplate template, string gaPrefix, int startIndex=0)
        {
            var newItem = template.CreateItem(this, gaPrefix);

            //startIndex = startIndex <= MaxGASubAddress ? MaxGASubAddress + 1 : startIndex;
            newItem.ShiftGA(startIndex);

            //var maxId = newItemPart.GAs.Select(x => x.SubAddress).Max();
            //var newItemBlockLength = blockLength != 0 ? blockLength : maxId+1;
            //NextItemId += newItemBlockLength;

            Items.Add(newItem);
            return newItem;
        }

        public int GetNextStartingBlockIndex()
        {
            var blockCount = (double)(MaxGASubAddress + 1) / defaultBlockLength;
            var ceiled = (int)Math.Ceiling(blockCount);
            var next = ceiled * defaultBlockLength;

            //var nextBlockIndex = Convert.ToInt32(Math.Ceiling((double)(MaxGASubAddress + 1) / defaultBlockLength) * defaultBlockLength);
            return next;
        }


        //public List<GA> GetAllGAs() 
        //{
        //    var itemGAs = Items.SelectMany(x => x.GAs);
        //    var subGroups = itemGAs.GroupBy(x => x.SubGroup);

        //    var outputGAs = new List<GA>(itemGAs);

        //    if (FillGASpaces)
        //    {
        //        var fillBlockerGAs = new List<GA>();
        //        foreach (var group in subGroups)
        //        {
        //            var maxGroupId = NextItemId;
        //            var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.SubAddress != x));
        //            fillBlockerGAs.AddRange(freeIds.Select(id => new GA(group.Key, id, "Blocker")));
        //        }
        //        outputGAs.AddRange(fillBlockerGAs);
        //    }

        //    if (FillGAToEnd)
        //    {
        //        var fillToEndBlockerGAs = new List<GA>();
        //        foreach (var group in subGroups)
        //        {
        //            var maxGroupId = 256;
        //            var freeIds = Enumerable.Range(0, maxGroupId).Where(x => group.All(ga => ga.SubAddress != x));
        //            fillToEndBlockerGAs.AddRange(freeIds.Select(id => new GA(group.Key, id, "Blocker")));
        //        }
        //        outputGAs.AddRange(fillToEndBlockerGAs);
        //    }

        //    return [.. outputGAs.OrderBy(x => x.SubGroup.MainGroup.SubAddress).ThenBy(x => x.SubGroup.SubAddress).ThenBy(x => x.SubAddress)];

        //}

        public SubGroup GetOrCreateSubGroup(int subAddress, string name="Neue MIttelgruppe")
        {
            var res = GetSubGroup(subAddress);
            return res ?? SubGroup.Create(subAddress,name, this);
        }

        public SubGroup GetOrCreateSubGroup(SubGroupTemplate subGroupTemplate)
        {
            var res = GetSubGroup(subGroupTemplate);

            return res ?? SubGroup.Create(subGroupTemplate, this);
        }
        public SubGroup? GetSubGroup(SubGroupTemplate subGroupTemplate)
        {
            return GetSubGroup(subGroupTemplate.SubAddress);
        }

        public SubGroup? GetSubGroup(int subGroupAddress)
        {
            return SubGroups.Where(x => x.SubAddress == subGroupAddress).FirstOrDefault();
        }

        //public string GetCSVString()
        //{
        //    var gas = GetAllGAs();

        //    var grouped = gas.GroupBy(x => x.SubGroup);

        //    var outputStr = string.Join(";", new string[] { Name, "", "", SubAddress.ToString(), "", "" })+"\n";

        //    foreach (var subGroup in grouped)
        //    {
        //        var ordered = subGroup.OrderBy(x => x.SubAddress).ToList();

        //        outputStr += string.Join(";", new string[] { "", subGroup.Key.Name, "", SubAddress.ToString(), subGroup.Key.SubAddress.ToString(), "" }) + "\n";
        //        outputStr += string.Join("\n",ordered.Select(g => string.Join(";", new string[] { "", "", g.Name, SubAddress.ToString(), subGroup.Key.SubAddress.ToString(), g.SubAddress.ToString() })));
        //        outputStr += "\n";
        //    }
        //    return outputStr;            
        //}

        public override string Address  => SubAddress.ToString(); 

        public string ListBoxString => AddressName + " ("+DefaultBlockLength + " / " + (MaxGASubAddress == -1 ? "-" : MaxGASubAddress) + ")";

        
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
            return Create(t.SubAddress, t.Name, mGroup);
        }

        public static SubGroup Create(int subAddress, string name, MainGroup mainGroup)
        {
            return new SubGroup(subAddress, name, mainGroup);
        }
    }
}
