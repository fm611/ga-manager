
using System.Text.Json.Serialization;


namespace GroupAddress.Core
{

    public class ItemTemplate : TopLevelCollection
    {

        [JsonConstructor]
        private ItemTemplate() : base() { }

        public ItemTemplate(string name, IEnumerable<GA> gas) : base(name, gas)
        {
        }


    }
}
