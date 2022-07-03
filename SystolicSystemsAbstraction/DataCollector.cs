using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    internal class DataCollector
    {
        List<DataCollectorStorage> _storages;
        public DataCollector()
        {
            _storages = new List<DataCollectorStorage>();
        }
        public DataCollectorStorage AddStorage()
        {
            var res = new DataCollectorStorage();
            _storages.Add(res);
            return res;
        }
        public string GetCollectedValues()
        {
            var line = "";
            
            foreach(var storage in _storages)
            {
                foreach(var item in storage.Data)
                {
                    line += item.ToString() + ' ';
                }
                line += '\n';
            }
            
            return line;
        }
    }
}
