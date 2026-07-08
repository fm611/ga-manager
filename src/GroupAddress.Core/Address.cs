using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class Address : IEquatable<Address?>
    {
        private int mainGroup;
        private int middleGroup;
        private int gA;

        public event EventHandler<EventArgs> Changed;
        public Address() : this(-1,-1,-1)
        {
        }

        public Address(int middleGroup, int ga) : this(-1,middleGroup,ga)
        {
        }

        public Address(int mainGroup, int middleGroup, int ga)
        {
            MainGroup = mainGroup;
            MiddleGroup = middleGroup;
            GA = ga;
        }

        public int MainGroup { get => mainGroup; set { 
                mainGroup = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }
        public int MiddleGroup { get => middleGroup; 
            set { 
                middleGroup = value;
                Changed?.Invoke(this, EventArgs.Empty);
            } 
        }
        public int GA { get => gA; set 
            { 
                gA = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public Address Clone()
        {
            return new Address(MainGroup, MiddleGroup, GA);
        }

        public override string ToString()
        {
            return (MainGroup < 0 ? "x" : MainGroup) + "/" + MiddleGroup + "/" + GA;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Address);
        }

        public bool EqualsWithoutMainGroup(Address? other)
        {
            return other is not null &&
                   MiddleGroup == other.MiddleGroup &&
                   GA == other.GA;
        }

        public bool Equals(Address? other)
        {
            return other is not null &&
                   MainGroup == other.MainGroup &&
                   MiddleGroup == other.MiddleGroup &&
                   GA == other.GA;
        }

        public static bool operator ==(Address? left, Address? right)
        {
            return EqualityComparer<Address>.Default.Equals(left, right);
        }

        public static bool operator !=(Address? left, Address? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
