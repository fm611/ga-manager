using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public struct GA
    {
        public int HG { get; set; }
        public int MG { get; set; }
        public int UG { get; set; }

        public override string ToString()
        {
            return HG+"/"+MG+"/"+UG;
        }
    }
}
