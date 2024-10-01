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
        public SubGroupTemplate subGroupTemplate { get; set; }
        public string AddonString { get; set; } = "";

        public string Id { get; set; }

        public GATemplatePart() {

            Id = Guid.NewGuid().ToString();
        }

        private GATemplatePart(SubGroupTemplate subGroupTemplate, string addonString = "") : this()
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
        public string Id { get; private set; }
        public string BaseString { get; set; } = "";

        public List<GATemplatePart> GAParts { get; set; }

        public GATemplate()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GATemplate(string baseString, IEnumerable<GATemplatePart> parts) : this()
        {
            BaseString = baseString;
            GAParts = parts.ToList();
        }
        public GATemplate(string baseString, GATemplatePart part) : this()
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
        public SubGroup SubGroup { get; set; }
        public string Name { get; set; }
        public int SubAddress { get; set; }
        public string Id { get; set; }

        public GA() {
            Id = Guid.NewGuid().ToString();
        }

        public GA(SubGroup subGroup, int subAddress, string name) :this()
        {
            SubGroup = subGroup;
            SubAddress = subAddress;
            Name = name;

            SubGroup.GAs.Add(this);
        }

        public string Address => SubGroup.Address + "/" + SubAddress;

        public string AddressName => Address + " - " + Name;

        public override string ToString()
        {
            return Address + " - " + Name;
        }

    }


}
