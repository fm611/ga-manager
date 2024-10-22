using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class AddressElement
    {
        public string Id { get; set; }
        public int SubAddress { get; set; }
        public string Name { get; set; }

        public AddressElement() {
            Id = Guid.NewGuid().ToString();
            SubAddress = 0;
            Name = "";
        }

        public AddressElement(int subAddress, string name) : this()
        {
            SubAddress = subAddress;
            Name = name;
        }
    }
}
