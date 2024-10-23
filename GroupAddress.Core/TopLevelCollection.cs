using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class TopLevelCollection
    {
        private List<GA> _gAs = [];
        public IReadOnlyCollection<GA> GAs => _gAs.AsReadOnly();

        public string[] SubGroupNames { get; private set; } = new string[8];


        public string Id { get; set; }
        public string Name { get; set; } = "";
        public TopLevelCollection() {
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



        public void SetSubGroupname(int addresse,  string subgroupname)
        {
            SubGroupNames[addresse] = subgroupname;
        }

        public void AddGA(GA ga) {
            if(!_gAs.Any(x => x.Addresse == ga.Addresse))
                _gAs.Add(ga);
        }

        public void AddGARange(IEnumerable<GA> gas) {
            foreach (var item in gas)
            {
                AddGA(item);
            }
        }

        public void RemoveGA(GA ga)
        {
            _gAs.Remove(ga);
        }

        public void ShiftGA(int s)
        {
            foreach (var item in _gAs)
            {
                item.Addresse.GA += s;
            }
        }




    }
}
