
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace GroupAddress.Core
{


    public class MainGroup : TopLevelCollection
    {



        private List<Item> _items = [];
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

        public int MaxGASubAddress => GAs.Select(x => x.Addresse.GA).DefaultIfEmpty(-1).Max();

        public int SubAddress { get; private set; }


        public int DefaultBlockLength { get; set; } = 1;

        private MainGroup() : base() { }

        public MainGroup(int subAddress, string name,string[] subGroupNames, int defaultBlockLength = 1) : base(name)
        {
            DefaultBlockLength = defaultBlockLength > 0 ? defaultBlockLength : 1;
            SubAddress = subAddress;

            for (int i = 0; i < 8; i++)
                SetSubGroupname(i, subGroupNames[i]);
        }

        public void SetSubAddress(int subAddress)
        {
            throw new NotImplementedException();
        }

        public new void AddGA(GA ga)
        {
            ga.Addresse.MainGroup = SubAddress;
            base.AddGA(ga);
        }
        public new void AddGARange(IEnumerable<GA> gas)
        {
            foreach(var g in gas)
            {
                AddGA(g);
            }
        }

        public Item? AddItem(ItemTemplate template, string gaPrefix, int startIndex = 0)
        {
            var newItem = new Item(gaPrefix);
            var gas = template.GAs.Select(x => x.CloneWithPrefix(gaPrefix)).ToList();
            newItem.AddGARange(gas);
            newItem.ShiftGA(startIndex);

            if (GAs.Any(x => GAs.Select(y => y.Addresse.MiddleGroup).Contains(x.Addresse.MiddleGroup) &&
                            GAs.Select(y => y.Addresse.GA).Contains(x.Addresse.GA)))
                return null;

            AddGARange(gas);
            _items.Add(newItem);
            return newItem;
        }

        public int GetNextStartingBlockIndex()
        {
            var blockCount = (double)(MaxGASubAddress + 1) / DefaultBlockLength;
            var ceiled = (int)Math.Ceiling(blockCount);
            var next = ceiled * DefaultBlockLength;

            return next;
        }

        public new void RemoveGA(GA ga)
        {
            var item = _items.Where(x => x.GAs.Contains(ga)).FirstOrDefault();
            if (item != null)
                item.RemoveGA(ga);

            base.RemoveGA(ga);
        }

        //public override string Address => SubAddress.ToString();

        public string AddressName => SubAddress + " - " + Name;
        public string ListBoxString => AddressName + " (" + DefaultBlockLength + " / " + (MaxGASubAddress == -1 ? "-" : MaxGASubAddress) + ")";


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


        //public static bool IsValidSubAddress(int addr)
        //{
        //    return addr >= 0 && addr < 32;
        //}

    }


}
