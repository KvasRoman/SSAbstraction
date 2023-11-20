using System;
using System.Collections.Generic;
using System.Linq;
using SSAbstraction;
namespace TestSS
{
    internal class Program
    {
        static void KSACloneTest(PElement PE)
        {
            PElement testPE = PE.Clone();
            PElement testPE1 = PE.Clone();
            PElement testPE2 = PE.Clone();
            testPE1.ConnectTo(testPE, 0, 0);
            testPE2.ConnectTo(testPE, 1, 0);
            KSArray testKSArray = new KSArray(new List<PElement>() { testPE, testPE1, testPE2 });
            KSArray testKSArrayClone = testKSArray.Clone(testKSArray);
            testKSArray.AddInputValues(new List<List<double>>()
            {
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0}
            });
            testKSArrayClone.AddInputValues(new List<List<double>>()
            {
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0},
                new List<double> { 1,2,3,0}
            });
            Console.WriteLine("Main");
            testKSArray.TactStart();
            Console.WriteLine(testKSArray.GetRegistersValues());
            Console.WriteLine("Clone");
            testKSArrayClone.TactStart();
            Console.WriteLine(testKSArrayClone.GetRegistersValues());
            Console.WriteLine("Main");
            testKSArray.TactStart();
            Console.WriteLine(testKSArray.GetRegistersValues());
            Console.WriteLine("Clone");
            testKSArrayClone.TactStart();
            Console.WriteLine(testKSArrayClone.GetRegistersValues());

            Console.WriteLine();
            Console.WriteLine(testKSArray.GetCollectorsValues());
            Console.WriteLine();

            Console.WriteLine("Main");
            testKSArray.TactStart();
            Console.WriteLine(testKSArray.GetRegistersValues());
            Console.WriteLine("Clone");
            testKSArrayClone.TactStart();
            Console.WriteLine(testKSArray.GetRegistersValues());
            Console.WriteLine(testKSArrayClone.GetRegistersValues());
            Console.WriteLine(testKSArrayClone.GetRegistersValues());
            Console.WriteLine();
            Console.WriteLine(testKSArray.GetCollectorsValues());
            Console.WriteLine("Main");
            testKSArray.TactStart();
            Console.WriteLine(testKSArray.GetRegistersValues());
            Console.WriteLine("Clone");
            testKSArrayClone.TactStart();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(testKSArrayClone.GetCollectorsValues());
            Console.WriteLine();
        }
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
            KSACloneTest(pe);
            return;
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
