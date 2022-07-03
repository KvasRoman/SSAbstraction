using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class InputNode : IDataReciever, IIdentifieble, IRepeater
    { 
        List<Edge> _edges;
        double? _value = null;
        double? _lockedValue = null;
        string _id;
        
        bool _isLocked;
        public bool isLocked => _isLocked;
        public InputNode(params Edge[] edges)
        {
            _edges = edges.ToList();
            _isLocked = false;
            _id = Guid.NewGuid().ToString();
        }
        public InputNode()
        {
            _edges = new List<Edge>();
            _isLocked = false;
            _id = Guid.NewGuid().ToString();
        }
        public string Id => _id;

        public List<Edge> edges => _edges;

        public void AddEdge(Edge edge)
        {
            _edges.Add(edge);
        }

        public void ProcessValue(double value)
        {
            if(isLocked)
                _lockedValue = value;
            else 
                _value = value;
        }

        public void SendValue()
        {
            double value = _value == null ? 0 : _value.Value;
            foreach (Edge e in _edges)
            {
                e.TransferValue(value);
            }
        }
        public void Lock() { _isLocked = true; }
        public void Unlock() { _isLocked = false; _value = _lockedValue; }
    }
}
