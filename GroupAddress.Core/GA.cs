using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class GA: IComparable<GA>
    {
        private string name = "";
        private Addresse addresse = new Addresse();

        public event EventHandler<EventArgs> Changed;
        public string Id { get; set; }
        public string Name { get => name; 
            set {
                name = value;
               Addresse_Changed(this, EventArgs.Empty);
            }
        }
        public Addresse Addresse { get => addresse; 
            private set { 
                addresse = value;
                addresse.Changed += Addresse_Changed;
            } 
        }

        private void Addresse_Changed(object? sender, EventArgs e)
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public string? ItemId { get; set; }

        private GA()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GA(int middleGroupAddress, int subAddress, string name) : this()
        {
            Name = name;
            Addresse = new(middleGroupAddress, subAddress);
        }
        public GA(Addresse addresse, string name) : this()
        {
            Name = name;
            Addresse = addresse;
        }

        public void Shift(int x)
        {
            Addresse.GA += x;
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
