using System;
using System.Collections.Generic;
using System.Linq;
using SSAbstraction;
namespace TestSS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            FunctionA2 funcEl = new FunctionA2((a, b) => a + b);
            Register register = new Register();
            OutputNode[] outputs = new OutputNode[1]
            {
                new OutputNode()
            };
            Edge[] edges = new Edge[]
            {
                new Edge(funcEl),
                new Edge(funcEl),
                new Edge(register),
                new Edge(outputs[0])
            };
            funcEl.AddEdge(edges[2]);
            register.AddEdge(edges[3]);
            InputNode[] inputs = new InputNode[2]
            {
                new InputNode(edges[0]),
                new InputNode(edges[1])
            };
            PElement pe = new PElement(inputs.ToList(), outputs.ToList(), new List<Register>() { register }, new List<FunctionA2>() { funcEl });
            PElement pe1 = pe.Clone();
            PElement pe2 = pe.Clone();

            KSArray kSArray = new KSArray(new List<PElement>() { pe });
            kSArray.AddInputValues(new List<List<double>>()
            {
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0}
            });
            kSArray.TactStart();
            Console.WriteLine(kSArray.GetRegistersValue());
            kSArray.TactStart();

            Console.WriteLine(kSArray.GetRegistersValue());
            kSArray.TactStart();

            Console.WriteLine(kSArray.GetRegistersValue());
            kSArray.TactStart();
            Console.WriteLine(kSArray.GetRegistersValue());
            Console.WriteLine(kSArray.GetCollectorsValues());
            Console.WriteLine(kSArray.InputWidth);
            Console.WriteLine(pe.GetRegistersValue() + pe1.GetRegistersValue() + pe2.GetRegistersValue());
        }
    }
}
