using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupAddress.Core
{


    public class GetGATemplate : GATemplate
    {
        public GetGATemplate(SubGroupTemplate sGroup, string baseString) : base(null, sGroup, baseString, null, null)
        {
        }
        public GetGATemplate(SubGroupTemplate sGroup, string baseString, string getAddonString) : base(null, sGroup, baseString, null,getAddonString)
        {
        }
    }

    public class SetGATemplate : GATemplate
    {
        public SetGATemplate(SubGroupTemplate mGroup, string baseString) : base(null, mGroup, baseString)
        {
        }
        public SetGATemplate(SubGroupTemplate mGroup, string baseString, string setAddonString) : base(mGroup,null, baseString, setAddonString,null)
        {
        }
    }


    public class GATemplate(SubGroupTemplate? setMGroup, SubGroupTemplate? getMGroup, string baseString, string? setAddonString = null, string? getAddonString = null)
    {
        public string? BaseString { get; private set; } = baseString;
        public string? SetAddonString { get; private set; } = setAddonString;
        public string? GetAddonString { get; private set; } = getAddonString;

        public SubGroupTemplate? GetSubGroupTemplate { get; private set; } = getMGroup;
        public SubGroupTemplate? SetSubGroupTemplate { get; private set; } = setMGroup;

        public IEnumerable<GA> CreateGA(MainGroup mGroup,int id, string preString)
        {
            var output = new List<GA>();

            if (SetSubGroupTemplate != null) output.Add(new GA(mGroup.GetSubGroup(SetSubGroupTemplate), id, string.Join("_", new[] { preString, BaseString, SetAddonString }.Where(s => !string.IsNullOrEmpty(s)))));
            if (GetSubGroupTemplate != null) output.Add(new GA(mGroup.GetSubGroup(GetSubGroupTemplate), id, string.Join("_", new[] { preString, BaseString, GetAddonString }.Where(s => !string.IsNullOrEmpty(s)))));

            return output;
        }
    }

    public class GA 
    {
        public SubGroup MiddleGroup { get; protected set; }
        public string Name { get; protected set; }
        public int Id { get; protected set; }


        public GA(SubGroup mGroup, int id, string name) 
        {
            MiddleGroup = mGroup;
            Id = id;
            Name = name;
        }

        public string Address => MiddleGroup + "/" + Id;

        public override string ToString()
        {
            return Address + " - " + Name;
        }

    }

    //public class GATemplateGroup : List<GATemplate>
    //{
    //    public string BaseString { get; private set; }
    //    public string SetAddonString { get; private set; }
    //    public string GetAddonString { get; private set; }


    //    private List<GATemplateGroup> TemplateGAs { get; } = [];

    //    public GATemplateGroup() { }

    //}

}
