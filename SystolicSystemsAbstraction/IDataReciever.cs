using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public interface IDataReciever : IIdentifieble
    {
        void ProcessValue(double value);
    }
}
