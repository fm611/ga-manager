using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupAddress.Core
{

    public class GA 
    {
        public SubGroup SubGroup { get; set; }
        public string SubGroupId { get; set; }
        public string Name { get; set; }
        public int SubAddress { get; set; }
        public string Id { get; set; }
        public MainGroup MainGroup { get; set; }
        public Item? Item { get; set; }

        public GA() {
            Id = Guid.NewGuid().ToString();
        }

        public GA(SubGroup subGroup, int subAddress, string name) :this()
        {
            SubGroup = subGroup;
            SubAddress = subAddress;
            Name = name;
            MainGroup = subGroup.MainGroup;

            SubGroup.AddGA(this);
        }

        //public string Address => SubGroup.Address + "/" + SubAddress;

        //public string AddressName => Address + " - " + Name;

        //public override string ToString()
        //{
        //    return Address + " - " + Name;
        //}

    }


}
