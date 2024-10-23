using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class SubGroupTemplate : AddressElement
    {
        public ItemTemplateSimple ItemTemplate { get; set; }

        public SubGroupTemplate(ItemTemplateSimple itemTemplate, int subAddress, string name) : base(subAddress,name)
        {
            ItemTemplate = itemTemplate;
        }
    }

}
