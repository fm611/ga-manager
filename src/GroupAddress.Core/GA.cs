using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class GA: IComparable<GA>
    {
        private string name = "";

        public event EventHandler<EventArgs>? Changed;
        public string Id { get; set; }
        public string Name { get => name; 
            set {
                name = value;
               Address_Changed(this, EventArgs.Empty);
            }
        }
        [JsonInclude]
        [JsonPropertyName("Address")]
        private Address address = new Address();
        [JsonIgnore]
        public Address Address { get => address; 
            private set { 
                address = value;
                address.Changed += Address_Changed;
            } 
        }

        private void Address_Changed(object? sender, EventArgs e)
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public string? GroupId { get; set; }

        [JsonConstructor]
        private GA()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GA(int middleGroupAddress, int subAddress, string name) : this()
        {
            Name = name;
            Address = new(middleGroupAddress, subAddress);
        }
        public GA(Address address, string name) : this()
        {
            Name = name;
            Address = address;
        }

        public void Shift(int x)
        {
            Address.GA += x;
        }


        public GA CloneWithPrefix(string preString)
        {
            var newGA = new GA();
            newGA.Name = string.Join("_", [preString, Name]);
            newGA.Address = Address.Clone();
            return newGA;
        }

        public override string ToString()
        {
            return Address + " - " + Name;
        }

        public int CompareTo(GA? other)
        {
            if(other == null) return 1;
            if(other == this) return 0;

            return Address.ToString().CompareTo(other.Address.ToString());
        }

        public string AddressName => Address + " - " + Name;

    }


}
