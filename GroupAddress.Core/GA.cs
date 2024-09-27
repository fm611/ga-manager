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



        //public string SetPostString { get; private set; } = "";
        //public string GetPostString { get; private set; } = "";

        //public SubGroupTemplate? SetSubGroupTemplate { get; private set; }
        //public SubGroupTemplate? GetSubGroupTemplate { get; private set; }

        //public GASpacerType SpacerType { get; private set; } = GASpacerType.None;


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



        //// Single GA
        //public GATemplate(SubGroupTemplate subGroupTemplate, string baseString, string addonString="")
        //{
        //    SetSubGroupTemplate = subGroupTemplate;
        //    SetPostString = string.Join("_", new[] { baseString, addonString }.Where(s => !string.IsNullOrEmpty(s)));
        //}



        //// Single GA + Blocker
        //public GATemplate(SubGroupTemplatePair subGroupTemplatePair, GASpacerType blockerType, string baseString, string addonString = "") : 
        //    this(subGroupTemplatePair.SetGroup, subGroupTemplatePair.GetGroup, blockerType,baseString,addonString)
        //{ }

        //    public GATemplate(SubGroupTemplate setMGroup, SubGroupTemplate getMGroup, GASpacerType spacerType, string baseString, string addonString="")
        //{
        //    GetSubGroupTemplate = getMGroup;
        //    SetSubGroupTemplate = setMGroup;
        //    SpacerType = spacerType;

        //    var postString = string.Join("_", new[] { baseString, addonString }.Where(s => !string.IsNullOrEmpty(s)));

        //    SetPostString = postString;
        //    GetPostString = postString;
        //}



        //// SET + GET GA
        //public GATemplate(SubGroupTemplatePair subGroupTemplatePair, string baseString, string setAddonString = "", string getAddonString = "") :
        //    this(subGroupTemplatePair.SetGroup, subGroupTemplatePair.GetGroup, baseString, setAddonString, getAddonString)
        //{ }

        //public GATemplate(SubGroupTemplate setMGroup, SubGroupTemplate getMGroup, string baseString, string setAddonString = "", string getAddonString = "")
        //{
        //    SetPostString = string.Join("_", new[] { baseString, setAddonString }.Where(s => !string.IsNullOrEmpty(s)));
        //    GetPostString = string.Join("_", new[] { baseString, getAddonString }.Where(s => !string.IsNullOrEmpty(s)));

        //    GetSubGroupTemplate = getMGroup;
        //    SetSubGroupTemplate = setMGroup;
        //}

        private string CreateGAString(string preString, GATemplatePart part, string seperator="_")
        {
            return string.Join(seperator, new[] { preString, BaseString, part.AddonString }.Where(s => !string.IsNullOrEmpty(s)));
        }

        public IEnumerable<GA> CreateGA(MainGroup mGroup, int id, string preString)
        {
            return GAParts.Select(p => new GA(mGroup.GetOrCreateSubGroup(p.subGroupTemplate), id, CreateGAString(preString, p)));
        }

        
        
        //public IEnumerable<GA> CreateGA(MainGroup mGroup,int id, string preString)
        //{
        //    var output = new List<GA>();

        //    var setName = SpacerType == GASpacerType.SetSpacer ? SetPostString : preString + "_" + SetPostString;
        //    var getName = SpacerType == GASpacerType.GetSpacer ? GetPostString : preString + "_" + GetPostString;

        //    if (SetSubGroupTemplate != null && SpacerType != GASpacerType.SetSpacer) output.Add(new GA(mGroup.GetOrCreateSubGroup(SetSubGroupTemplate), id, setName));
        //    if (GetSubGroupTemplate != null && SpacerType != GASpacerType.GetSpacer) output.Add(new GA(mGroup.GetOrCreateSubGroup(GetSubGroupTemplate), id, getName));

        //    return output;
        //}
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
