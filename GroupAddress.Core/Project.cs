
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;


namespace GroupAddress.Core
{
    public class Project
    {
        public event EventHandler<EventArgs>? Changed;
        
        public DateTime Created { get; set; }
        public DateTime Saved { get; set; }

        [JsonInclude]
        [JsonPropertyName("MainGroups")]
        private List<MainGroup> _mainGroups = [];
        [JsonIgnore]
        public IReadOnlyCollection<MainGroup> MainGroups => _mainGroups.AsReadOnly();
        [JsonInclude]
        [JsonPropertyName("ItemTemplates")]
        private List<ItemTemplate> _itemTemplates = [];
        [JsonIgnore]
        public IReadOnlyCollection<ItemTemplate> ItemTemplates => _itemTemplates.AsReadOnly();
        [JsonInclude]
        [JsonPropertyName("Items")]
        private List<Item> _items = [];
        [JsonIgnore]
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

        public Project() {
            Created = DateTime.Now;
        }

        public void AddMainGroup(MainGroup mainGroup)
        {
            _mainGroups.Add(mainGroup);
            mainGroup.Changed += (sender,e) => OnChange();
            OnChange();
        }

        public void ResetEventBindings()
        {
            foreach (var mg in _mainGroups)
            {
                mg.Changed -= (sender, e) => OnChange();
                mg.Changed += (sender, e) => OnChange();
            }
            foreach (var it in _itemTemplates)
            {
                it.Changed -= (sender, e) => OnChange();
                it.Changed += (sender, e) => OnChange();
            }
        }


        private void OnChange()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
        
        public bool RemoveMainGroup(MainGroup mainGroup)
        {
            var res = _mainGroups.Remove(mainGroup);
            if (res)
            {
                OnChange();
                mainGroup.Changed -= (sender,e) => OnChange();
            }
            return res;
        }

        public void AddItemTemplate(ItemTemplate itemTemplate)
        {
            _itemTemplates.Add(itemTemplate);
            itemTemplate.Changed += (sender,e) => OnChange();
            OnChange();
        }

        public bool RemoveItemTemplate(ItemTemplate itemTemplate)
        {
            var res = _itemTemplates.Remove(itemTemplate);
            if (res) {
                OnChange();
                itemTemplate.Changed -= (sender,e) => OnChange();
            }
            return res;
        }


        public void AddItem(Item item)
        {
            _items.Add(item);
            item.Changed += (sender, e) => OnChange();
            OnChange();
        }
        public bool RemoveItem(Item item)
        {
            var res = _items.Remove(item);
            if (res)
            {
                OnChange();
                item.Changed -= (sender, e) => OnChange();
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

        public static Project? FromJson(string json)
        {
            var obj = JsonSerializer.Deserialize<Project>(json, 
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }
            );
            if (obj != null)
            {
                obj.ResetEventBindings();
            }
            return obj;
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
                proj.AddItem(item);            

            #endregion


            return proj;

        }

               

    }
}