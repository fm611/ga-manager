using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupAddress.Core
{

    public class GA: IComparable<GA>
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public Addresse Addresse { get; set; } = new Addresse();

        private GA()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GA(int middleGroupAddress, int subAddress, string name) : this()
        {
            Name = name;
            Addresse = new(middleGroupAddress, subAddress);
        }

        public GA CloneWithPrefix(string preString)
        {
            var newGA = new GA();
            newGA.Name = string.Join("_", [preString, Name]);
            newGA.Addresse = Addresse.Clone();
            return newGA;
        }

        public override string ToString()
        {
            return Addresse + " - " + Name;
        }

        public int CompareTo(GA? other)
        {
            if(other == null) return 1;
            if(other == this) return 0;

            return Addresse.ToString().CompareTo(other.Addresse);
        }

        public string AddressName => Addresse + " - " + Name;

    }


}
