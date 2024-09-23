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
        public int Id { get; protected set; }
        public string Name { get; protected set; }

    }


    public class TemplateGA : GABase
    {
        public string PostFix { get; private set; }

        public TemplateGA(MiddleGroup middleGroup, string postFix)
        {
            MiddleGroup = middleGroup;
            PostFix = postFix;
        }

        public GA GetGA(int ug, string preFix)
        {
            return new GA(this, ug, preFix);
        }
    }

    public class GA : GABase
    {

        public GA(TemplateGA template, int id, string preFix) 
        {
            MiddleGroup = template.MiddleGroup;
            Id = id;

            Name = preFix + "_" + template.PostFix;
        }
    }

    public class GaGroup
    {
        private List<MiddleGroup> MiddleGroups { get; } = [];
        private List<GA> GAs { get; } = [];

        public GaGroup() { }

        public void Add(MiddleGroup middleGroup, GA ga)
        {
            MiddleGroups.Add(middleGroup);
            GAs.Add(ga);
        }
    }
}
