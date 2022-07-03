using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public class FunctionA2 : IDataReciever, IRepeater, IIdentifieble
    {
        Func<double, double, double> _function2;
        Func<double,double, double, double> _function3;
        List<Edge> _edges;
        int _argsCount;
        int _argsMax;
        List<double> _args;
        string _id;
        public string Id => _id;

        public List<Edge> edges => _edges;

        public FunctionA2(Func<double, double, double> function)
        {
            _argsCount = 0;
            _argsMax = 2;
            _args = new List<double>();
            _edges = new List<Edge>();
            _function2 = function == null ? (a, b) => { return 0; } : function;
            _id = Guid.NewGuid().ToString();
        }

        public FunctionA2(Func<double, double, double, double> function)
        {
            _argsCount = 0;
            _argsMax = 2;
            _args = new List<double>();
            _edges = new List<Edge>();
            _function3 = function == null ? (a, c, b) => { return 0; } : function;
            _id = Guid.NewGuid().ToString();
        }
        public FunctionA2 Clone()
        {
            return new FunctionA2(_function2);
        }
        public void AddEdge(Edge edge)
        {
            _edges.Add(edge);
        }

        public void ProcessValue(double value)
        {
            if (_argsCount >= _argsMax) throw new Exception("The argument limit has been faced");
            else
            {
                _args.Add(value);
                _argsCount++;
                if(_argsCount == _argsMax)
                {
                    _argsCount = 0;
                    double res = _function2(_args[0], _args[1]);
                    foreach (Edge edge in _edges)
                        edge.TransferValue(res);
                    _args.Clear();
                }    
            }
        }
    }
}
