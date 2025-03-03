using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class TopLevelCollection
    {
        public event EventHandler<EventArgs>? Changed;
        [JsonInclude]
        [JsonPropertyName("GAs")]
        private List<GA> _gAs = [];
        [JsonIgnore]
        public IReadOnlyCollection<GA> GAs => _gAs.AsReadOnly();

        public string[] SubGroupNames { get; private set; } = new string[8];

        public string Id { get; set; }
        public string Name { get; set; } = "";

        public int MinGASubAddress => GAs.Select(x => x.Address.GA).DefaultIfEmpty(-1).Min();
        public int MaxGASubAddress => GAs.Select(x => x.Address.GA).DefaultIfEmpty(-1).Max();

        public int SubAddress { get; protected set; } = -1;

        public TopLevelCollection()
        {
            Id = Guid.NewGuid().ToString();
            SubGroupNames = Enumerable.Range(0, 8).Select(x => "").ToArray();
        }

        public TopLevelCollection(string name) : this()
        {
            Name = name;
        }
        public TopLevelCollection(string name, IEnumerable<GA> gas) : this()
        {
            Name = name;
            gas.ToList().ForEach(x => _gAs.Add(x));
        }

        public void SetSubGroupname(int address, string subgroupname)
        {
            SubGroupNames[address] = subgroupname;
        }
        public void SetSubAddress(int subAddress)
        {
            SubAddress = subAddress;
            foreach (var ga in _gAs)
            {
                ga.Address.MainGroup = subAddress;
            }
        }


        public void AddGA(GA ga)
        {
            if (_gAs.Any(x => x.Address == ga.Address)) return;

            ga.Address.MainGroup = SubAddress;
            _gAs.Add(ga);
            ga.Changed += Ga_Changed;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void Ga_Changed(object? sender, EventArgs e)
        {
           Changed?.Invoke(sender, e);
        }

        public void AddGARange(IEnumerable<GA> gas)
        {
            foreach (var ga in gas)
            {
                AddGA(ga);
            }
        }
        public virtual void RemoveGA(GA ga)
        {
            _gAs.Remove(ga);
            ga.Changed -= Ga_Changed;
            Changed?.Invoke(this, EventArgs.Empty);
        }
        public void ShiftGA(int s)
        {
            foreach (var ga in _gAs)
            {
                ga.Address.GA += s;
            }
        }






    }
}
