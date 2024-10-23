using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class AddressElement
    {
        public string Id { get; set; }
        public int SubAddress { get; set; } = -1;
        public string Name { get; set; } = "";

        protected AddressElement() {
            Id = Guid.NewGuid().ToString();
        }

        public AddressElement(int subAddress, string name) : this()
        {
            SubAddress = subAddress;
            Name = name;
        }
    }
}
