
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;


namespace GroupAddress.Core
{
    public class Project : ObservableObject, INotifyDeepChange
    {
        public event DeepChangeEventHandler? DeepChange;

        public event EventHandler<EventArgs>? ProjectSaved;

        [JsonIgnore]
        public bool Dirty { get; private set; }
        public DateTime Created { get; set; }
        public DateTime Saved { get; set; }

        [JsonInclude]
        [JsonPropertyName("MainGroups")]
        private ObservableCollection<MainGroup> _mainGroups = [];
        [JsonIgnore]
        public ReadOnlyObservableCollection<MainGroup> MainGroups => new ReadOnlyObservableCollection<MainGroup>(_mainGroups);
<<<<<<< HEAD

=======
        
>>>>>>> c00e77ca1cf5c422779a88b24963e96a1bbc3289
        [JsonInclude]
        [JsonPropertyName("GroupTemplates")]
        private ObservableCollection<GroupTemplate> _groupTemplates = [];
        [JsonIgnore]
        public ReadOnlyObservableCollection<GroupTemplate> GroupTemplates => new ReadOnlyObservableCollection<GroupTemplate>(_groupTemplates);

        [JsonInclude]
        [JsonPropertyName("Groups")]
        private ObservableCollection<Group> _groups = [];
        [JsonIgnore]
        public ReadOnlyObservableCollection<Group> Groups => new ReadOnlyObservableCollection<Group>(_groups);


        public Project()
        {
            Created = DateTime.Now;
            Dirty = false;
<<<<<<< HEAD
<<<<<<< HEAD
            _mainGroups.CollectionChanged += (sender, e) => OnChange();
            _groupTemplates.CollectionChanged += (sender, e) => OnChange();
            _groups.CollectionChanged += (sender, e) => OnChange();
        }


        public void AddMainGroup(MainGroup mainGroup)
        {
            _mainGroups.Add(mainGroup);
            mainGroup.Changed += (sender,e) => OnChange();
        }
=======
            _mainGroups.CollectionChanged += _mainGroups_CollectionChanged;
        }
        private void _mainGroups_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Test");
        }


>>>>>>> c00e77ca1cf5c422779a88b24963e96a1bbc3289
=======
            _mainGroups.CollectionChanged += _mainGroups_CollectionChanged;
        }
        private void _mainGroups_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Test");
        }


>>>>>>> c00e77ca1cf5c422779a88b24963e96a1bbc3289

        public void ResetEventBindings()
        {
            if (_mainGroups != null)
            {
                _mainGroups.CollectionChanged -= (sender, e) => OnChange();
                _mainGroups.CollectionChanged += (sender, e) => OnChange();

                foreach (var mg in _mainGroups)
                {
                    mg.Changed -= (sender, e) => OnChange();
                    mg.Changed += (sender, e) => OnChange();
                }
            }

            if (_groupTemplates != null)
            {
                _groupTemplates.CollectionChanged -= (sender, e) => OnChange();
                _groupTemplates.CollectionChanged += (sender, e) => OnChange();

                foreach (var it in _groupTemplates)
                {
                    it.Changed -= (sender, e) => OnChange();
                    it.Changed += (sender, e) => OnChange();
                }
            }

            if (_groups != null)
            {
                _groups.CollectionChanged -= (sender, e) => OnChange();
                _groups.CollectionChanged += (sender, e) => OnChange();

                foreach (var it in _groups)
                {
                    it.Changed -= (sender, e) => OnChange();
                    it.Changed += (sender, e) => OnChange();
                }
            }


        }


        private void OnChange([CallerMemberName] string? callerName = null)
        {
            Dirty = true;
            DeepChange?.Invoke(this, new DeepChangeEventArgs(callerName));
        }

        private void OnSave()
        {
            ProjectSaved?.Invoke(this, EventArgs.Empty);
        }

        public void SetUndirty()
        {
            Dirty = false;
            OnSave();
        }
        public void AddMainGroup(MainGroup mainGroup)
        {
            _mainGroups.Add(mainGroup);
            mainGroup.Changed += (sender, e) => OnChange();
            OnChange();
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

        public void AddGroupTemplate(GroupTemplate groupTemplate)
        {
            _groupTemplates.Add(groupTemplate);
            groupTemplate.Changed += (sender,e) => OnChange();
            OnChange();
        }

        public bool RemoveGroupTemplate(GroupTemplate groupTemplate)
        {
            var res = _groupTemplates.Remove(groupTemplate);
            if (res) {
                OnChange();
                groupTemplate.Changed -= (sender,e) => OnChange();
            }
            return res;
        }


        public void AddGroup(Group group)
        {
            _groups.Add(group);
            group.Changed += (sender, e) => OnChange();
            OnChange();
        }
        public bool RemoveGroup(Group group)
        {
            var res = _groups.Remove(group);
            if (res)
            {
                OnChange();
                group.Changed -= (sender, e) => OnChange();
            }
            return res;
        }


        public List<Group> GetGroups(MainGroup? mainGroup)
        {
            return Groups.ToList();

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

            #region Init GroupTemplates

            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var groupTemplates = new List<GroupTemplate>() {
                DefaultGroupTemplates.Light,
                DefaultGroupTemplates.LightDimm,
                DefaultGroupTemplates.LightTW,
                DefaultGroupTemplates.LightRGBW
            };

            for (int i = 0; i < groupTemplates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                if (proj.GroupTemplates.Any(t => t.Id == guid)) continue;

                var template = groupTemplates[i];
                template.Id = guid;
                proj.AddGroupTemplate(template);

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

            #region Init Group

            var mGroup1 = proj.MainGroups.First(x => x.SubAddress == 1);

            var group = mGroup1.AddGroup(DefaultGroupTemplates.Light, "EG_HWR_Licht_Decke");
            if (group != null)
                proj.AddGroup(group);            

            #endregion


            return proj;

        }

               

    }
}