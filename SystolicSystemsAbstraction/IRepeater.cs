using SSAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public interface IRepeater
    {
        List<Edge> edges { get; }
        public void AddEdge(Edge edge);
    }
}
