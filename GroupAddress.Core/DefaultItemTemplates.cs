using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    //public static class DefaultItemTemplates
    //{

    //    public static ItemTemplate Light = new ItemTemplate("Licht allgemein - Steuerung",
    //    [
    //        new GATemplate("Schalten", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "Sperren1", "Sperren_Status")),
    //        new GATemplate("", GATemplatePart.Create(SubGroupTemplate.SetMisc, "Sperren2")),
    //        new GATemplate("Szene", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //    ]);

    //    public static ItemTemplate LightDimm = new ItemTemplate("Licht dimmbar - Steuerung",
    //    [
    //        new GATemplate("Schalten", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),

    //        new GATemplate("DimmHell", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmHell", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),

    //        new GATemplate("", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "Sperren1", "Sperren_Status")),
    //        new GATemplate("", GATemplatePart.Create(SubGroupTemplate.SetMisc, "Sperren2")),
    //        new GATemplate("Szene", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),

    //        new GATemplate("Bitszene1", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene2", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene3", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene4", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Sequenz1", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz2", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //    ]);


    //    public static ItemTemplate LightTW = new ItemTemplate("Licht TW - Steuerung",
    //    [
    //        new GATemplate("Schalten", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("HCL", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),

    //        new GATemplate("DimmHell", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmHell", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("DimmTemp_P", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmTemp_P", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("DimmTemp_K", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmTemp_K", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),

    //        new GATemplate("", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "Sperren1", "Sperren_Status")),
    //        new GATemplate("", GATemplatePart.Create(SubGroupTemplate.SetMisc, "Sperren2")),
    //        new GATemplate("Szene", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),

    //        new GATemplate("Bitszene1", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene2", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene3", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene4", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Sequenz1", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz2", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //    ]);

    //    public static ItemTemplate LightRGBW = new ItemTemplate("Licht RGBW - Steuerung",
    //    [
    //        new GATemplate("Schalten", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("HCL", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("R", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("G", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("B", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),
    //        new GATemplate("W", GATemplatePart.CreatePair(SubGroupTemplate.SwitchPair, "", "Status")),

    //        new GATemplate("Dimm_H", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_H", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("Dimm_S", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_S", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("Dimm_V", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_V", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),

    //        new GATemplate("Dimm_R", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_R", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("Dimm_G", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_G", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("Dimm_B", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_B", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("Dimm_W", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("Dimm_W", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),

    //        new GATemplate("DimmTemp_P", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmTemp_P", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),
    //        new GATemplate("DimmTemp_K", GATemplatePart.Create(SubGroupTemplate.SetValue, "relativ")),
    //        new GATemplate("DimmTemp_K", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "absolut", "Wert")),

    //        new GATemplate("HSV", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "", "Wert")),
    //        new GATemplate("RGB", GATemplatePart.CreatePair(SubGroupTemplate.ValuePair, "", "Wert")),

    //        new GATemplate("RGBW", GATemplatePart.Create(SubGroupTemplate.GetValue, "Wert")),


    //        new GATemplate("", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "Sperren1", "Sperren_Status")),
    //        new GATemplate("", GATemplatePart.Create(SubGroupTemplate.SetMisc, "Sperren2")),
    //        new GATemplate("Szene", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),

    //        new GATemplate("Bitszene1", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene2", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene3", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Bitszene4", GATemplatePart.Create(SubGroupTemplate.SetMisc, "")),
    //        new GATemplate("Sequenz1", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz2", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz3", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz4", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz5", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status")),
    //        new GATemplate("Sequenz6", GATemplatePart.CreatePair(SubGroupTemplate.MiscPair, "", "Status"))
    //    ]);


    //}
}
