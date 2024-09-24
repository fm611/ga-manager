using GroupAddress.Core;
using System.Collections.Generic;

namespace GroupAddress.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var lightCommonMainGroup = new MainGroup(1, "Licht allgemein");
            var lightCommonMain_Switch_MiddleGroup = new MiddleGroup(1, "Schalten", lightCommonMainGroup);
            var lightCommonMain_SwitchStatus_MiddleGroup = new MiddleGroup(2, "Schalten Status", lightCommonMainGroup);
            var lightCommonMain_Lock_MiddleGroup = new MiddleGroup(3, "Sperren", lightCommonMainGroup);
            var lightCommonMain_LockStatus_MiddleGroup = new MiddleGroup(4, "Sperren Status", lightCommonMainGroup);

            var lightDimmMainGroup = new MainGroup(2, "Licht dimmbar");
            var lightDimm_BrightnessRel_MiddleGroup = new MiddleGroup(1, "Bright relativ", lightDimmMainGroup);
            var lightDimm_BrigthnessAbs_MiddleGroup = new MiddleGroup(2, "Bright absolut", lightDimmMainGroup);
            var lightDimm_BrightnesseValue_MiddleGroup = new MiddleGroup(3, "Bright Wert", lightDimmMainGroup);

            var lightTWMainGroup = new MainGroup(3, "Licht TW");
            var lightTW_TempRel_MiddleGroup = new MiddleGroup(1, "Temp relativ", lightTWMainGroup);
            var lightTW_TempAbs_MiddleGroup = new MiddleGroup(2, "Temp absolut", lightTWMainGroup);
            var lightTW_TempValue_MiddleGroup = new MiddleGroup(3, "Temp Wert", lightTWMainGroup);

            var lightRGBWMainGroup = new MainGroup(4, "Licht RGBW");
            var lightRGBW_Switch_MiddleGroup = new MiddleGroup(1, "Schalten", lightRGBWMainGroup);
            var lightRGBW_SwitchStatus_MiddleGroup = new MiddleGroup(2, "Schalten Status", lightRGBWMainGroup);
            var lightRGBW_DimmRel_MiddleGroup = new MiddleGroup(3, "Dimm relativ", lightRGBWMainGroup);
            var lightRGBW_DimmAbs_MiddleGroup = new MiddleGroup(4, "Dimm absolut", lightRGBWMainGroup);
            var lightRGBW_DimmValue_MiddleGroup = new MiddleGroup(5, "Dimm Wert", lightRGBWMainGroup);


            var lightCommonTemplate = new Item();

            lightCommonTemplate.GATemplateGroups.AddRange([ 
                [
                    new TemplateGA(lightCommonMain_Switch_MiddleGroup, "Schalten"),
                    new TemplateGA(lightCommonMain_SwitchStatus_MiddleGroup, "Schalten_Status")
                ],
                [
                    new TemplateGA(lightCommonMain_Lock_MiddleGroup, "Sperren1"),
                    new TemplateGA(lightCommonMain_LockStatus_MiddleGroup, "Sperren1_Status")
                ],
                [
                    new TemplateGA(lightCommonMain_Lock_MiddleGroup, "Sperren2"),
                    new TemplateGA(lightCommonMain_LockStatus_MiddleGroup, "Sperren2_Status")
                ]
            ]);
            
            var dimmLightTemplate = new Item(lightCommonTemplate);
            dimmLightTemplate.GATemplateGroups.AddRange([
                [
                    new TemplateGA(lightDimm_BrightnessRel_MiddleGroup, "Hell_rel"),
                    new TemplateGA(lightDimm_BrigthnessAbs_MiddleGroup, "Hell_abs"),
                    new TemplateGA(lightDimm_BrightnesseValue_MiddleGroup, "Hell_Wert")
                ]
            ]);


            var dimmTWTemplate = new Item(dimmLightTemplate);
            dimmTWTemplate.GATemplateGroups.AddRange([
                [
                    new TemplateGA(lightTW_TempRel_MiddleGroup, "TempP_rel"),
                    new TemplateGA(lightTW_TempAbs_MiddleGroup, "TempP_abs"),
                    new TemplateGA(lightTW_TempValue_MiddleGroup, "TempP_Wert"),
                ],
                [
                    new TemplateGA(lightTW_TempRel_MiddleGroup, "TempK_rel"),
                    new TemplateGA(lightTW_TempAbs_MiddleGroup, "TempK_abs"),
                    new TemplateGA(lightTW_TempValue_MiddleGroup, "TempK_Wert"),
                ]
            ]);
                               


            var rgbwTemplate = new Item(dimmTWTemplate);
            rgbwTemplate.GATemplateGroups.AddRange([
                [
                    new TemplateGA(lightRGBW_Switch_MiddleGroup, "R_Schalten" ),
                    new TemplateGA(lightRGBW_SwitchStatus_MiddleGroup, "R_Schalten_Status" )
                ],
                [
                    new TemplateGA(lightRGBW_Switch_MiddleGroup, "G_Schalten" ),
                    new TemplateGA(lightRGBW_SwitchStatus_MiddleGroup, "G_Schalten_Status" )
                ],
                [
                    new TemplateGA(lightRGBW_Switch_MiddleGroup, "B_Schalten" ),
                    new TemplateGA(lightRGBW_SwitchStatus_MiddleGroup, "B_Schalten_Status" )
                ],
                [
                    new TemplateGA(lightRGBW_Switch_MiddleGroup, "W_Schalten" ),
                    new TemplateGA(lightRGBW_SwitchStatus_MiddleGroup, "W_Schalten_Status" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "H_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "H_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "H_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "S_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "S_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "S_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "R_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "R_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "R_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "G_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "G_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "G_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "B_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "B_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "B_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "W_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "W_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "W_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmRel_MiddleGroup, "W_rel" ),
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "W_abs" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "W_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "HSV" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "HSV_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmAbs_MiddleGroup, "RGB" ),
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "RGB_Wert" )
                ],
                [
                    new TemplateGA(lightRGBW_DimmValue_MiddleGroup, "RGBW_Wert" )
                ]
            ]);


            var gas = dimmTWTemplate.CreateGA("EG_WZ_Spots");

        }
    }
}
