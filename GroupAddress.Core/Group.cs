namespace GroupAddress.Core
{
    public abstract class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class MainGroup : Group
    {
        public List<SubGroup> SubGroups { get; set; } = [];

        public MainGroup(int id, string name) : base(id, name)
        {
        }

        public SubGroup? GetSubGroup(SubGroupTemplate? subGroupTemplate)
        {
            if (subGroupTemplate == null) return null;
            return SubGroups
                .Where(x => x.Id == subGroupTemplate.Id)
                .DefaultIfEmpty(SubGroup.Create(subGroupTemplate, this))
                .First(); 
        }
    }

    public class SubGroupTemplate : Group
    {
        public static SubGroupTemplate Switch = new SubGroupTemplate(1, "Switch");
        public static SubGroupTemplate SwitchStatus = new SubGroupTemplate(2, "Switch Status");
        public static SubGroupTemplate SetValue = new SubGroupTemplate(3, "Set Value");
        public static SubGroupTemplate GetValue = new SubGroupTemplate(4, "Get Value");
        public static SubGroupTemplate SetMisc = new SubGroupTemplate(5, "Set Misc");
        public static SubGroupTemplate GetMisc = new SubGroupTemplate(6, "Get Misc");

        public static List<SubGroupTemplate> DefaultLightTemplates = [Switch, SwitchStatus, SetValue, GetValue, SetMisc, GetMisc];


        public SubGroupTemplate(int id, string name) : base(id, name)
        {

        }
    }


    public class SubGroup : Group
    {
        public MainGroup MainGroup { get; }

        public SubGroup(int id, string name, MainGroup mainGroup) : base(id, name)
        {
            MainGroup = mainGroup;
            mainGroup.SubGroups.Add(this);
        }

        public SubGroup(SubGroupTemplate template, MainGroup mGroup) : this(template.Id, template.Name, mGroup)
        {
        }

        public override string ToString()
        {
            return MainGroup.Id + "/" + Id;
        }

        public static SubGroup Create(SubGroupTemplate t, MainGroup mGroup)
        {
            return new SubGroup(t.Id, t.Name, mGroup);
        }
    }
}
