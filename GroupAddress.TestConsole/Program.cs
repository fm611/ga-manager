using GroupAddress.Core;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace GroupAddress.TestConsole
{
    internal class Program
    {


        static void Main(string[] args)
        {

            var p = Project.GetSampleProject();

            var json = p.GetJson();

            string folder = Path.GetDirectoryName(Environment.ProcessPath);
            string path = Path.Join(folder, "sampleProject.json");

            FileStream createStream = File.Create(path);
            JsonSerializer.Serialize(createStream, p, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });

            //var db = new AppDbContext();
            //db.Database.Migrate();
            //db.InitData();



            //var mg = db.MainGroups
            //    .Include(x => x.GAs)
            //    .Include(x => x.Items)
            //    .First(x => x.SubAddress == 1);

            //var template = DefaultItemTemplates.Light;

            //mg.AddItem(template, "EG_HWR_Licht_Decke");

            //mg.AddItem(template, "EG_KU_Licht_Esstisch");


            ////var sg = mg.SubGroups.FirstOrDefault(x => x.SubAddress == 0);

            ////sg.Name = "Zentral 000";

            //db.SaveChanges();


            //var ga = mg.GAs.ElementAt(4);

            //mg.RemoveGA(ga);

            //db.SaveChanges();

            //Console.WriteLine("Done");





            //var dbContext = new AppDbContext();

            //dbContext.Database.EnsureCreated();

            //dbContext.InitData();


            //var lightCommonMainGroup = new MainGroup(1, "Licht allgemein");

            //var lightDimmMainGroup = new MainGroup(2, "Licht dimmbar");

            //var lightTWMainGroup = new MainGroup(3, "Licht TW");

            //var lightRGBWMainGroup = new MainGroup(4, "Licht RGBW");

            //var hwr_light = DefaultItemTemplates.Light.CreateItem([lightCommonMainGroup], "EG_HWR_Licht_Decke");


            //lightCommonMainGroup.AddItem(ItemPartTemplate.Light, "EG_HWR_Licht_Decke", 20);
            //lightCommonMainGroup.AddItem(ItemPartTemplate.Light, "EG_GWC_Licht_Decke", 10);


            //lightRGBWMainGroup.AddItem(ItemPartTemplate.LightRGBW, "EG_WZ_LED_Band_1");

            //lightDimmMainGroup.AddItem(ItemPartTemplate.LightDimm, "EG_KU_Esstisch", 10);
            //lightDimmMainGroup.AddItem(ItemPartTemplate.LightDimm, "EG_EZ_Esstisch", 10);



            //lightRGBWMainGroup.FillGASpaces = true;

            //var gas = lightRGBWMainGroup.GetAllGAs();

            //var lightCommonStr = lightRGBWMainGroup.GetCSVString();

            //var json = JsonSerializer.Serialize(ItemPartTemplate.Light);

        }
    }
}

