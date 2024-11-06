using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    public class ItemTemplate : TopLevelCollection
    {

        private ItemTemplate() : base() { }

        public ItemTemplate(string name, IEnumerable<GA> gas) : base(name, gas)
        {

        }


    }
}
