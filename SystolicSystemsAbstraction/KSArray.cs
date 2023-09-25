using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAbstraction
{
    public enum ArrayForm
    {
        box,
        line,
        hexahon
    }
    public class KSArray
    {
        int _inputWidth;
        int _tactsAvailable;
        List<List<double>> _storedValues;
        
        List<PElement> _array;
        public int InputWidth => _inputWidth;
        List<InputNode> inputNodes;
        DataCollector _collector;
        public KSArray(ArrayForm form, PElement pe, int numberOfPE, Action<PElement, PElement> connect)
        {
            
            _inputWidth = 0;
            _collector = new DataCollector();
            _storedValues = new List<List<double>>();
            _array = new List<PElement>();
            inputNodes = new List<InputNode>();
            if (form == ArrayForm.line)
            {
                for (int i = 0; i < numberOfPE; i++)
                    _array.Add(pe.Clone());
                for (int i = 0; i < numberOfPE - 1; i++)
                    connect(_array[i], _array[i + 1]);
            }
            else
            {
                _array.Add(pe);
            }
            List<string> inNodesList = new List<string>();
            foreach (PElement element in _array)
            {
                foreach (var i in element.InNodes)
                {
                    inNodesList.Add(i.Id);
                    inputNodes.Add(i);
                }
            }
            _inputWidth = inNodesList.Count;
            foreach (PElement element in _array)
            {
                foreach (var o in element.OutNodes)
                {
                    if (o.edges.Count == 0)
                        o.edges.Add(new Edge(_collector.AddStorage()));
                    foreach (var e in o.edges)
                    {
                        if (inNodesList.Remove(e.To) && inputNodes.RemoveAll((n) => n.Id == e.To) == 1) _inputWidth--;
                    }
                }
            }
        }
        public KSArray()
        {
        }
        public KSArray(List<PElement> array)
        {
            _inputWidth = 0;
            _collector = new DataCollector();
            _storedValues = new List<List<double>>();
            inputNodes = new List<InputNode>();
            _array = array;
            List<string> inNodesList = new List<string>();
            foreach (PElement element in array)
            {
                foreach (var i in element.InNodes)
                {
                    inNodesList.Add(i.Id);
                    inputNodes.Add(i);
                }
            }
            _inputWidth = inNodesList.Count;
            foreach (PElement element in array)
            {
                foreach (var o in element.OutNodes)
                {
                    if (o.edges.Count == 0)
                        o.edges.Add(new Edge(_collector.AddStorage()));
                    foreach (var e in o.edges)
                    {
                        if (inNodesList.Remove(e.To) && inputNodes.RemoveAll((n) => n.Id == e.To) == 1) _inputWidth--;
                    }
                }
            }
        }
        public KSArray KSArrayMerge(KSArray array)
        {
            return new KSArray(array._array.Concat(_array).ToList());
        }
        public KSArray Clone(KSArray array)
        {
            List<PElement> peList = new List<PElement>();
            foreach(var pe in array._array)
            {
                peList.Add(pe.Clone());
            }
            for(var i = 0;i < peList.Count; i++)
            {
                for(var k = 0;k < peList[i].OutNodes.Count; k++)
                {
                    for(var j = 0;j < array._array[i].OutNodes[k].edges.Count; j++)
                    {
                        var edge = array._array[i].OutNodes[k].edges[j];
                        int arrayIndex = 0;
                        foreach(var arrayElement in array._array)
                        {
                            int ToIndex;
                            if((ToIndex = arrayElement.InNodes.FindIndex(inNode => inNode.Id == edge.To)) != -1)
                            {
                                peList[i].OutNodes[k].edges.Add(new Edge(peList[arrayIndex].InNodes[ToIndex]));
                                break;
                            }
                            arrayIndex++;
                        }
                        
                    }
                }
            }

            return new KSArray(peList);
        }
        public string GetRegistersValues()
        {
            string res = "";
            foreach (var element in _array)
                res += element.GetRegistersValue() + " ";
            return res;
        }
        public void AddInputValues(List<List<double>> values)
        {
            if (InputWidth != values.Count)
                throw new Exception("Input values width is not equal input width");
            for(var i = 0;i < values.Count; i++)
            {
                if (_storedValues.Count <= i) _storedValues.Add(new List<double>());
                _storedValues[i].AddRange(values[i]);
                if (i >= inputNodes.Count)
                    break;
            }
            _tactsAvailable = _storedValues[0].Count;
            foreach(var element in _storedValues)
            {
                if(_tactsAvailable > element.Count) 
                    _tactsAvailable = element.Count; 
            }
        }
        public void TactStart()
        {
            if (_tactsAvailable <= 0) throw new Exception("There is no data");
            for(int i = 0; i < inputNodes.Count; i++)
            {
                inputNodes[i].ProcessValue(_storedValues[i][0]);
                _storedValues[i].RemoveAt(0);
            }
            foreach (var el in _array)
                el.Lock();
            foreach (var el in _array)
                el.Process();
            foreach (var el in _array)
                el.Unlock();
            _tactsAvailable--;
        }
        public string GetCollectorsValues()
        {
            return _collector.GetCollectedValues();
        }
        public List<List<double>> GetCollectorsValuesDouble()
        {
            return _collector.GetOutputCollection();
        }
        public string GetRegistersValue()
        {
            string res = "";
            foreach(var row in _array)
            {
                res += row.GetRegistersValue();
                res += '\n';
            }

            return res;
        }
    }
}
