using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class Register: IDataReciever, IRepeater, IIdentifieble
    {
        double _value;
        List<Edge> _edges;
        string _id;
        public string Id => _id;

        public List<Edge> edges => _edges;

        public Register()
        {
            _edges = new List<Edge>();
            _value = 0;
            _id = Guid.NewGuid().ToString();
        }
        public Register Clone()
        {
            return new Register();
        }
        public double GetValue() { 
            return _value; 
        }
        public void AddEdge(Edge edge)
        {
            _edges.Add(edge);
        }
        public void ProcessValue(double value)
        { 
            foreach (Edge edge in _edges)
                edge.TransferValue(this._value);
            this._value = value;
        }

        public void SetValue(double value)
        {
            this._value = value;
        }
    }
}
