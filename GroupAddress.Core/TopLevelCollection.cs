using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public abstract class TopLevelCollection
    {
        private List<GA> _gas = [];
        public IReadOnlyCollection<GA> GAs => _gas.AsReadOnly();


        private List<string> _subGroupNames = [];
        public IReadOnlyCollection<string> SubGroupNames => _subGroupNames.AsReadOnly();


        public string Id { get; set; }
        public string Name { get; set; } = "";
        public TopLevelCollection() {
            Id = Guid.NewGuid().ToString();
            _subGroupNames = Enumerable.Range(0, 8).Select(x => "").ToList();
        }

        public TopLevelCollection(string name) : this()
        {
            Name = name;
        }
        public TopLevelCollection(string name, IEnumerable<GA> gas) : this()
        {
            Name = name;
            gas.ToList().ForEach(x => _gas.Add(x));
        }



        public void SetSubGroupname(int addresse,  string subgroupname)
        {
            _subGroupNames[addresse] = subgroupname;
        }

        public void AddGA(GA ga) {
            if(!_gas.Any(x => x.Addresse == ga.Addresse))
                _gas.Add(ga);
        }

        public void AddGARange(IEnumerable<GA> gas) {
            foreach (var item in gas)
            {
                AddGA(item);
            }
        }

        public void ShiftGA(int s)
        {
            foreach (var item in _gas)
            {
                item.Addresse.GA += s;
            }
        }




    }
}
