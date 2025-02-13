using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class TopLevelCollection
    {
        public event EventHandler<EventArgs>? Changed;
        private List<GA> _gAs = [];
        public IReadOnlyCollection<GA> GAs => _gAs.AsReadOnly();

        public string[] SubGroupNames { get; private set; } = new string[8];

        public string Id { get; set; }
        public string Name { get; set; } = "";

        public int MinGASubAddress => GAs.Select(x => x.Addresse.GA).DefaultIfEmpty(-1).Min();
        public int MaxGASubAddress => GAs.Select(x => x.Addresse.GA).DefaultIfEmpty(-1).Max();

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

        public void SetSubGroupname(int addresse, string subgroupname)
        {
            SubGroupNames[addresse] = subgroupname;
        }
        public void SetSubAddress(int subAddress)
        {
            SubAddress = subAddress;
            foreach (var ga in _gAs)
            {
                ga.Addresse.MainGroup = subAddress;
            }
        }


        public void AddGA(GA ga)
        {
            if (_gAs.Any(x => x.Addresse == ga.Addresse)) return;

            ga.Addresse.MainGroup = SubAddress;
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
            foreach (var item in gas)
            {
                AddGA(item);
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
                ga.Addresse.GA += s;
            }
        }






    }
}
