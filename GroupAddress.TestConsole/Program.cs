using GroupAddress.Core;
using System.Collections.Generic;

namespace GroupAddress.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var lightCommonMainGroup = new MainGroup(1, "Licht allgemein");

            var lightDimmMainGroup = new MainGroup(2, "Licht dimmbar");

            var lightTWMainGroup = new MainGroup(3, "Licht TW");

            var lightRGBWMainGroup = new MainGroup(4, "Licht RGBW");

            lightCommonMainGroup.AddItem(ItemTemplate.Light, "EG_HWR_Licht_Decke", 20);
            lightCommonMainGroup.AddItem(ItemTemplate.Light, "EG_GWC_Licht_Decke", 10);


            lightRGBWMainGroup.AddItem(ItemTemplate.LightRGBW, "EG_WZ_LED_Band_1");

            lightDimmMainGroup.AddItem(ItemTemplate.LightDimm, "EG_KU_Esstisch",10);
            lightDimmMainGroup.AddItem(ItemTemplate.LightDimm, "EG_EZ_Esstisch",10);



            lightCommonMainGroup.FillGASpaces = true;

            var gas = lightDimmMainGroup.GetAllGAs();

            var lightCommonStr = lightCommonMainGroup.GetCSVString();

        }
    }
}
