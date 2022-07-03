using System;
using System.Collections.Generic;
using System.Linq;

namespace SSAbstraction
{
    /// <summary>
    ///     Process Element
    /// </summary>
    public class PElement : IIdentifieble
    {
        List<InputNode> _inNodes;
        List<OutputNode> _outNodes;
        List<FunctionA2> _funcs;
        List<Register> _registers;

        string _id;
        public string Id => _id;
        public List<InputNode> InNodes => _inNodes;
        public List<OutputNode> OutNodes => _outNodes;
        public void ConnectTo(PElement pe, int inNodeNum, int outNodeNum)
        {
            _outNodes[outNodeNum].AddEdge(new Edge(pe.InNodes[inNodeNum]));
        }
        public PElement(List<InputNode> inNodes, List<OutputNode> outNodes,List<Register> registers , List<FunctionA2> funcs)
        {
            this._inNodes = inNodes.ToList();
            this._outNodes = outNodes.ToList();
            this._funcs = funcs.ToList();
            this._registers = registers.ToList();
            this._id = Guid.NewGuid().ToString();
        }
        public PElement Clone()
        {
            List<FunctionA2> funcs = new List<FunctionA2>();
            foreach(var func in _funcs)
                funcs.Add(func.Clone());
            List<Register> regs = new List<Register>();
            foreach(var reg in _registers)
                regs.Add(reg.Clone());
            List<OutputNode> outNodes = new List<OutputNode> ();
            foreach (var outNode in _outNodes)
                outNodes.Add(new OutputNode());
            List<InputNode> inNodes = new List<InputNode> ();
            foreach (var inNode in _inNodes)
                inNodes.Add(new InputNode());
            
            for(var i = 0;i < _inNodes.Count; i++)
            {
                for(var j = 0;j < _inNodes[i].edges.Count; j++)
                {
                    IDataReciever resIDR = null;
                    int resI;
                    if ((resI = _funcs.FindIndex((f) => f.Id == _inNodes[i].edges[j].To)) != -1) {
                        resIDR = funcs[resI];
                    }
                    if ((resI = _registers.FindIndex((f) => f.Id == _inNodes[i].edges[j].To)) != -1) {
                        resIDR = regs[resI];
                    }
                    if ((resI = _outNodes.FindIndex((f) => f.Id == _inNodes[i].edges[j].To)) != -1) {
                        resIDR = outNodes[resI];
                    }
                    if (resIDR == null) {
                        throw new Exception("there is no such IDR");
                    }
                    Edge res = new Edge(resIDR);

                    inNodes[i].AddEdge(res);
                }
            }
            for (var i = 0; i < _funcs.Count; i++)
            {
                for (var j = 0; j < _funcs[i].edges.Count; j++)
                {
                    IDataReciever resIDR = null;
                    int resI;
                    if ((resI = _funcs.FindIndex((f) => f.Id == _funcs[i].edges[j].To)) != -1)
                    {
                        resIDR = funcs[resI];
                    }
                    if ((resI = _registers.FindIndex((f) => f.Id == _funcs[i].edges[j].To)) != -1)
                    {
                        resIDR = regs[resI];
                    }
                    if ((resI = _outNodes.FindIndex((f) => f.Id == _funcs[i].edges[j].To)) != -1)
                    {
                        resIDR = outNodes[resI];
                    }
                    if (resIDR == null)
                    {
                        throw new Exception("there is no such IDR");
                    }
                    Edge res = new Edge(resIDR);

                    funcs[i].AddEdge(res);
                }
            }
            for (var i = 0; i < _registers.Count; i++)
            {
                for (var j = 0; j < _registers[i].edges.Count; j++)
                {
                    IDataReciever resIDR = null;
                    int resI;
                    if ((resI = _funcs.FindIndex((f) => f.Id == _registers[i].edges[j].To)) != -1)
                    {
                        resIDR = funcs[resI];
                    }
                    if ((resI = _registers.FindIndex((f) => f.Id == _registers[i].edges[j].To)) != -1)
                    {
                        resIDR = regs[resI];
                    }
                    if ((resI = _outNodes.FindIndex((f) => f.Id == _registers[i].edges[j].To)) != -1)
                    {
                        resIDR = outNodes[resI];
                    }
                    if (resIDR == null)
                    {
                        throw new Exception("there is no such IDR");
                    }
                    Edge res = new Edge(resIDR);

                    regs[i].AddEdge(res);
                }
            }
            return new PElement(inNodes, outNodes, regs, funcs);
        }
        public void Process()
        {
            foreach(var inNode in _inNodes)
            {
                inNode.SendValue();
            }
        }

        public string GetRegistersValue()
        {
            string res = "";
            foreach(var reg in _registers)
            {
                res += reg.GetValue();
            }
            return res;
        }
        public void Lock()
        {
            foreach (var inNode in _inNodes)
                inNode.Lock();
        }
        public void Unlock()
        {
            foreach (var inNode in _inNodes)
                inNode.Unlock();
        }
    }
}
