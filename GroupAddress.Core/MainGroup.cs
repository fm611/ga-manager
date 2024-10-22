
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace GroupAddress.Core
{


    public class MainGroup : AddressElement
    {
        private SubGroup[] _subGroups { get; set; } = [];
        public IReadOnlyCollection<SubGroup> SubGroups => _subGroups.AsReadOnly();


        private List<Item> _items = [];
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

        public IReadOnlyCollection<GA> GAs => _subGroups.SelectMany(x => x.GAs).ToList().AsReadOnly();

        public int MaxGASubAddress => GAs.Select(x => x.SubAddress).DefaultIfEmpty(-1).Max();


        public int DefaultBlockLength { get; set; } = 1;

        public MainGroup() : base() { }

        public MainGroup(int subAddress, string name,string[] subGroupNames, int defaultBlockLength = 1) : base(subAddress, name)
        {
            DefaultBlockLength = defaultBlockLength > 0 ? defaultBlockLength : 1;

            _subGroups = Enumerable
                .Range(0, 8)
                .Select(x => new SubGroup(x, subGroupNames.ElementAtOrDefault(x) ?? "N/A"))
                .ToArray();
        }




        //public Item AddItem(ItemTemplate template, string gaPrefix, int startIndex = 0)
        //{
        //    var newItem = template.CreateItem(this, gaPrefix);

        //    newItem.ShiftGA(startIndex);

        //    Items.Add(newItem);
        //    return newItem;
        //}

        //public int GetNextStartingBlockIndex()
        //{
        //    var blockCount = (double)(MaxGASubAddress + 1) / defaultBlockLength;
        //    var ceiled = (int)Math.Ceiling(blockCount);
        //    var next = ceiled * defaultBlockLength;

        //    return next;
        //}

        //public new void RemoveGA(GA ga)
        //{
        //    base.RemoveGA(ga);

        //    SubGroups
        //        .FirstOrDefault(x => x.Id == ga.SubGroupId)?
        //        .RemoveGA(ga);
        //}

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


        //public SubGroup GetOrCreateSubGroup(int subAddress, string name = "Neue MIttelgruppe")
        //{
        //    var res = GetSubGroup(subAddress);
        //    return res ?? SubGroup.Create(subAddress, name, this);
        //}

        //public SubGroup GetOrCreateSubGroup(SubGroupTemplate subGroupTemplate)
        //{
        //    var res = GetSubGroup(subGroupTemplate);

        //    return res ?? SubGroup.Create(subGroupTemplate, this);
        //}
        //public SubGroup? GetSubGroup(SubGroupTemplate subGroupTemplate)
        //{
        //    return GetSubGroup(subGroupTemplate.SubAddress);
        //}

        //public SubGroup? GetSubGroup(int subGroupAddress)
        //{
        //    return SubGroups.Where(x => x.SubAddress == subGroupAddress).FirstOrDefault();
        //}

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

        //public override string Address => SubAddress.ToString();

        //public string ListBoxString => AddressName + " (" + DefaultBlockLength + " / " + (MaxGASubAddress == -1 ? "-" : MaxGASubAddress) + ")";

        //public static bool IsValidSubAddress(int addr)
        //{
        //    return addr >= 0 && addr < 32;
        //}

    }


}
