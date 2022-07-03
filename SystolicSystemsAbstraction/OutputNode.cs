
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class OutputNode: IDataReciever, IIdentifieble, IRepeater
    {
        List<Edge> _edges;
        string _id;
        public OutputNode(params Edge[] edges)
        {
            _edges = edges.ToList();
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
            foreach (Edge edge in _edges)
                edge.TransferValue(value);
        }

    }
}
