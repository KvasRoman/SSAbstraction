using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class DataCollectorStorage : IDataReciever, IIdentifieble
    {
        string _id;
        public string Id => _id;
        List<double> _data;
        public List<double> Data => _data;
        public DataCollectorStorage()
        {
            _data = new List<double>();
            _id = Guid.NewGuid().ToString();
        }
        public void ProcessValue(double value)
        {
            _data.Add(value);
        }

    }
}
