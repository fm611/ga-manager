using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;

namespace GroupAddress.Core
{
    public class Project
    {
        public event EventHandler<EventArgs>? Changed;
        
        public DateTime Created { get; set; }
        public DateTime Saved { get; set; }


        private List<MainGroup> _mainGroups = [];
        public IReadOnlyCollection<MainGroup> MainGroups => _mainGroups.AsReadOnly();

        private List<ItemTemplate> _itemTemplates = [];
        public IReadOnlyCollection<ItemTemplate> ItemTemplates => _itemTemplates.AsReadOnly();

        public List<Item> Items { get; set; } = [];

        public Project() {
            Created = DateTime.Now;
        }


        public void AddMainGroup(MainGroup mainGroup)
        {
            _mainGroups.Add(mainGroup);
            mainGroup.Changed += MainGroup_Changed;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void MainGroup_Changed(object? sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
        }

        public bool RemoveMainGroup(MainGroup mainGroup)
        {
            var res = _mainGroups.Remove(mainGroup);
            if (res)
            {
                Changed?.Invoke(this, EventArgs.Empty);
                mainGroup.Changed -= MainGroup_Changed;
            }
            return res;
        }

        public void AddItemTemplate(ItemTemplate itemTemplate)
        {
            _itemTemplates.Add(itemTemplate);
            itemTemplate.Changed += ItemTemplate_Changed;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void ItemTemplate_Changed(object? sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
        }

        public bool RemoveItemTemplate(ItemTemplate itemTemplate)
        {
            var res = _itemTemplates.Remove(itemTemplate);
            if (res) {
                Changed?.Invoke(this, EventArgs.Empty);
                itemTemplate.Changed -= ItemTemplate_Changed;
            }
            return res;
        }


        public List<Item> GetItems(MainGroup? mainGroup)
        {
            if (mainGroup == null) return [];
            var mgItems = mainGroup.GAs.Where(x => x.ItemId !=null).Select(x => x.ItemId).ToList();

            return Items.Where(x => mgItems.Contains(x.Id)).ToList();
        }

        public string GetJson()
        {
            Saved = DateTime.Now;

            return JsonSerializer.Serialize(this,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
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
                //proj.ItemTemplates.Add(template);
                proj.AddItemTemplate(template);

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
                //proj.MainGroups.Add(template);
                proj.AddMainGroup(template);
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