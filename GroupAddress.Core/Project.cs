using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace GroupAddress.Core
{
    public class Project
    {
        
        //public string Name { get; set; }

        public List<MainGroup> MainGroups { get; set; } = [];

        public List<ItemTemplate> ItemTemplates { get; set; } = [];

        public List<Item> Items { get; set; } = [];

        public Project() { }

        //public Project() : this("") { }
        //public Project(string name) { Name = name; }


        public string GetJson()
        {
            return JsonSerializer.Serialize(this,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
                );
        }

        public static Project GetSampleProject()
        {
            var proj = new Project();

            #region Init ItemTemplates

            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var itemTemplates = new List<ItemTemplate>() {
                DefaultItemTemplates.Light,
                DefaultItemTemplates.LightDimm,
                DefaultItemTemplates.LightTW,
                DefaultItemTemplates.LightRGBW
            };

            for (int i = 0; i < itemTemplates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                if (proj.ItemTemplates.Any(t => t.Id == guid)) continue;

                var template = itemTemplates[i];
                template.Id = guid;
                proj.ItemTemplates.Add(template);

            }

            #endregion

            #region Init MainGroups

            guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var subGroupNames = new string[] {
                "Zentral",
                "Zentral Status",
                "Schalten",
                "Schalten Status",
                "SET Wert",
                "GET Wert",
                "SET Misc",
                "GET Misc"
                };

            var mainGroupTemplates = new List<MainGroup>() {
                new MainGroup(1, "Licht allgemein", subGroupNames,10),
                new MainGroup(2, "Licht dimmbar", subGroupNames,10),
                new MainGroup(3, "Licht TW", subGroupNames,10),
                new MainGroup(4, "Licht RGBW #1", subGroupNames,50)
            };

            for (int i = 0; i < mainGroupTemplates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                if (proj.MainGroups.Any(t => t.Id == guid)) continue;

                var template = mainGroupTemplates[i];
                template.Id = guid;
                proj.MainGroups.Add(template);
            }

            #endregion

            #region Init Item

            var mGroup1 = proj.MainGroups.First(x => x.SubAddress == 1);

            var item = mGroup1.AddItem(DefaultItemTemplates.Light, "EG_HWR_Licht_Decke");
            if (item != null)
                proj.Items.Add(item);            

            #endregion


            return proj;

        }


    }
}