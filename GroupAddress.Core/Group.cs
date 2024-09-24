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
        public List<MiddleGroup> MiddleGroups { get; set; } = [];

        public MainGroup(int id, string name) : base(id, name)
        {
        }
    }

    public class MiddleGroup : Group
    {
        public MainGroup MainGroup { get; }

        public MiddleGroup(int id, string name, MainGroup mainGroup) : base(id, name)
        {
            MainGroup = mainGroup;
            mainGroup.MiddleGroups.Add(this);
        }

        public override string ToString()
        {
            return MainGroup.Id + "/" + Id;
        }
    }
}
