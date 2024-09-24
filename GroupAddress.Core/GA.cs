using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class GABase
    {
        public MiddleGroup MiddleGroup { get; protected set; }
        public string Name { get; protected set; }

    }


    public class TemplateGA : GABase
    {
        public string PostString { get; private set; }

        public TemplateGA(MiddleGroup middleGroup, string postFix)
        {
            MiddleGroup = middleGroup;
            PostString = postFix;
        }

        public GA CreateGA(int ug, string preFix)
        {
            return new GA(this, ug, preFix);
        }
    }

    public class GA : GABase
    {
        public int Id { get; protected set; }

        public GA(TemplateGA template, int id, string preString) 
        {
            MiddleGroup = template.MiddleGroup;
            Id = id;
            Name = preString + "_" + template.PostString;
        }

        public string Address => MiddleGroup + "/" + Id;

    }

    public class GATemplateGroup : List<TemplateGA>
    {
        private List<GATemplateGroup> TemplateGAs { get; } = [];

        public GATemplateGroup() { }

    }

}
