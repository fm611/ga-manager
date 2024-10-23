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

        private List<GA> _gas = [];
        public IReadOnlyCollection<GA> GAs => _gas.AsReadOnly();

        //public override string Address => MainGroup.SubAddress + "/" + SubAddress;

        private SubGroup() :base() { }
    

        public SubGroup(MainGroup mainGroup, int subAddress, string name) : base(subAddress, name)
        {
            MainGroup = mainGroup;
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
