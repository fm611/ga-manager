using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{

    //public class GATemplatePart
    //{
    //    public SubGroupTemplate SubGroupTemplate { get; set; }
    //    public string AddonString { get; set; } = "";

    //    public string Id { get; set; }

    //    public string GATemplateId { get; set; }

    //    public GATemplatePart()
    //    {

    //        Id = Guid.NewGuid().ToString();
    //    }

    //    private GATemplatePart(SubGroupTemplate subGroupTemplate, string addonString = "") : this()
    //    {
    //        this.SubGroupTemplate = subGroupTemplate;
    //        AddonString = addonString;
    //    }

    //    public static GATemplatePart Create(SubGroupTemplate subGroupTemplate, string addonString = "")
    //    {
    //        return new GATemplatePart(subGroupTemplate, addonString);
    //    }

    //    public static GATemplatePart[] CreatePair(SubGroupTemplatePair pair, string setAddonString, string getAddonString)
    //    {
    //        return [new GATemplatePart(pair.SetGroup, setAddonString), new GATemplatePart(pair.GetGroup, getAddonString)];
    //    }

    //    public override string ToString()
    //    {
    //        return "x/" + SubGroupTemplate.SubAddress + "/x : " + AddonString;
    //    }
    //}

    //public class GATemplate
    //{
    //    public string Id { get; private set; }
    //    public string BaseString { get; set; } = "";

    //    public int SubAddress { get; set; }

    //    public string ItemTemplateId { get; set; }

    //    public List<GATemplatePart> GAParts { get; set; }

    //    public GATemplate()
    //    {
    //        Id = Guid.NewGuid().ToString();
    //    }

    //    public GATemplate(string baseString, IEnumerable<GATemplatePart> parts) : this()
    //    {
    //        BaseString = baseString;
    //        GAParts = parts.ToList();
    //    }
    //    public GATemplate(string baseString, GATemplatePart part) : this()
    //    {
    //        BaseString = baseString;
    //        GAParts = [part];
    //    }


    //    private string CreateGAString(string preString, GATemplatePart part, string seperator = "_")
    //    {
    //        return string.Join(seperator, new[] { preString, BaseString, part.AddonString }.Where(s => !string.IsNullOrEmpty(s)));
    //    }

    //    public IEnumerable<GA> CreateGA(MainGroup mGroup, string preString)
    //    {
    //        return GAParts.Select(p => new GA(mGroup.GetOrCreateSubGroup(p.SubGroupTemplate), SubAddress, CreateGAString(preString, p)));
    //    }

    //    public override string ToString()
    //    {
    //        return "x/x/" + SubAddress + " : " + BaseString;
    //    }

    //}

}
