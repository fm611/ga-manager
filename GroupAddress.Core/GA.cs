using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupAddress.Core
{

    public enum GASpacerType
    {
        None,
        SetSpacer,
        GetSpacer
    }

    public class GATemplatePart
    {
        public SubGroupTemplate subGroupTemplate { get; private set; }
        public string AddonString { get; private set; } = "";
        //public bool IsSpacer { get; private set; }

        private GATemplatePart(SubGroupTemplate subGroupTemplate)
        {
            this.subGroupTemplate = subGroupTemplate;
        }
        private GATemplatePart(SubGroupTemplate subGroupTemplate, string addonString)
        {
            this.subGroupTemplate = subGroupTemplate;
            AddonString = addonString;
            //IsSpacer = false;
        }

        public static GATemplatePart Create(SubGroupTemplate subGroupTemplate, string addonString = "")
        {
            return new GATemplatePart(subGroupTemplate, addonString);
        }
        //public static GATemplatePart CreateSpacer(SubGroupTemplate subGroupTemplate)
        //{
        //    return new GATemplatePart(subGroupTemplate) { IsSpacer = true };
        //}

        public static GATemplatePart[] CreatePair(SubGroupTemplatePair pair, string setAddonString, string getAddonString)
        {
            return [new GATemplatePart(pair.SetGroup, setAddonString), new GATemplatePart(pair.GetGroup, getAddonString)];
        }
    }

    public class GATemplate
    {
        public string BaseString { get; set; } = "";

        public List<GATemplatePart> GAParts { get; set; }

        public GATemplate(string baseString, IEnumerable<GATemplatePart> parts)
        {
            BaseString = baseString;
            GAParts = parts.ToList();
        }
        public GATemplate(string baseString, GATemplatePart part)
        {
            BaseString = baseString;
            GAParts = [part];
        }


        private string CreateGAString(string preString, GATemplatePart part, string seperator="_")
        {
            return string.Join(seperator, new[] { preString, BaseString, part.AddonString }.Where(s => !string.IsNullOrEmpty(s)));
        }

        public IEnumerable<GA> CreateGA(MainGroup mGroup, int id, string preString)
        {
            return GAParts.Select(p => new GA(mGroup.GetOrCreateSubGroup(p.subGroupTemplate), id, CreateGAString(preString, p)));
        }
     
    }

    public class GA 
    {
        public SubGroup SubGroup { get; protected set; }
        public string Name { get; protected set; }
        public int Id { get; set; }


        public GA(SubGroup subGroup, int id, string name) 
        {
            SubGroup = subGroup;
            Id = id;
            Name = name;

            SubGroup.GAs.Add(this);
        }

        public string Address => SubGroup + "/" + Id;

        public override string ToString()
        {
            return Address + " - " + Name;
        }

    }


}
