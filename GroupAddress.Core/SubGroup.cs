using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class SubGroup : AddressElement
    {
        public MainGroup MainGroup { get; set; }

        private readonly List<GA> _gas = [];
        public ReadOnlyCollection<GA> GAs => _gas.AsReadOnly();

        //public override string Address => MainGroup.SubAddress + "/" + SubAddress;

        public SubGroup() : base() { }

        //public SubGroup(int subAddress, string name, MainGroup mainGroup) : base(subAddress, name)
        //{
        //    MainGroup = mainGroup;
        //    mainGroup.SubGroups.Add(this);
        //}
        public SubGroup(int subAddress, string name) : base(subAddress, name)
        {
        }


        public void AddGA(GA ga)
        {
            if (!_gas.Any(x => x.Id == ga.Id)) _gas.Add(ga);
        }
        public void RemoveGA(string id)
        {
            var ga = _gas.FirstOrDefault(x => x.Id == id);
            if (ga == null) return;
            RemoveGA(ga);
        }
        public void RemoveGA(GA ga)
        {
            _gas.Remove(ga);
        }

        //public override string ToString()
        //{
        //    return Address + " - " + Name;
        //}

        //public static SubGroup Create(SubGroupTemplate t, MainGroup mGroup)
        //{
        //    return Create(t.SubAddress, t.Name, mGroup);
        //}

        //public static SubGroup Create(int subAddress, string name, MainGroup mainGroup)
        //{
        //    return new SubGroup(subAddress, name, mainGroup);
        //}

    }
}
