using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class Edge
    {
        IDataReciever _reciever;
        public string To { get => ((IIdentifieble)_reciever).Id; }
        public Edge(IDataReciever reciever)
        {
            _reciever = reciever;
        }
        public void TransferValue(double value)
        {
            _reciever.ProcessValue(value);
        }
    }
}
