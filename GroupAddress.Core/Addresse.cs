using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class Addresse : IEquatable<Addresse?>
    {
        public Addresse() : this(-1,-1,-1)
        {
        }

        public Addresse(int middleGroup, int ga) : this(-1,middleGroup,ga)
        {
        }

        public Addresse(int mainGroup, int middleGroup, int ga)
        {
            MainGroup = mainGroup;
            MiddleGroup = middleGroup;
            GA = ga;
        }

        public int MainGroup { get; set; }
        public int MiddleGroup { get; set; }
        public int GA { get; set; }

        public Addresse Clone()
        {
            return new Addresse(MainGroup, MiddleGroup, GA);
        }

        public override string ToString()
        {
            return MainGroup+"/"+MiddleGroup+"/"+GA;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Addresse);
        }

        public bool Equals(Addresse? other)
        {
            return other is not null &&
                   MainGroup == other.MainGroup &&
                   MiddleGroup == other.MiddleGroup &&
                   GA == other.GA;
        }

        public static bool operator ==(Addresse? left, Addresse? right)
        {
            return EqualityComparer<Addresse>.Default.Equals(left, right);
        }

        public static bool operator !=(Addresse? left, Addresse? right)
        {
            return !(left == right);
        }
    }
}
