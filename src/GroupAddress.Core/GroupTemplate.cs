
using System.Text.Json.Serialization;


namespace GroupAddress.Core
{

    public class GroupTemplate : TopLevelCollection
    {

        [JsonConstructor]
        private GroupTemplate() : base() { }

        public GroupTemplate(string name, IEnumerable<GA> gas) : base(name, gas)
        {
        }


    }
}
