using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace kek
{
    delegate KeyValuePair<TKey, TValue>GenerateElement<TKey,TValue>(int j);
    class TestCollections<TKey, TValue>
    {
        private List<TKey> _keyList;
        private List<string> _valueList;
        private Dictionary<TKey, TValue> _KVDictionary;
        private Dictionary<string, TValue> _SVDictionary;
        private GenerateElement<TKey, TValue> _generateElement;

        public TestCollections(int count, GenerateElement<TKey, TValue> j)
        {
            _keyList = new List<TKey>();
            _valueList = new List<string>();
            _KVDictionary = new Dictionary<TKey, TValue>();
            _SVDictionary = new Dictionary<string, TValue>();
            _generateElement = j;
            for(int i = 0; i < count; i++)
            {
                var element = _generateElement(i);
                _KVDictionary.Add(element.Key, element.Value);
                _SVDictionary.Add(element.Key.ToString(), element.Value);
                _keyList.Add(element.Key);
                _valueList.Add(element.Key.ToString());
            }
        }
        
        public static KeyValuePair<Team, ResearchTeam> GenerateElement(int j)
        {
            Team key = new Team("Team" + j, j % 11);
            ResearchTeam value = new ResearchTeam("RuCl" + j, "ResearchTeam" + j, j % 10, (TimeFrame)(j % 3));
            return new KeyValuePair<Team, ResearchTeam>(key, value);
        }
        public void SearchKV()
        {
            Console.WriteLine("\nIn keyList \nTime of searching:\n");
            TKey first = _keyList[0];
            TKey middle = _keyList[_keyList.Count / 2];
            TKey last = _keyList[_keyList.Count - 1];
            TKey none = _generateElement(_keyList.Count + 1).Key;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _keyList.Contains(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _keyList.Contains(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _keyList.Contains(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _keyList.Contains(none);
            sw.Stop();
            Console.WriteLine("For the element that there is no in list: " + sw.Elapsed);
        }

        public void SearchSV()
        {
            Console.WriteLine("\nIn list_str\nTime of the search:\n");

            var first = _valueList[0];
            var middle = _valueList[_valueList.Count / 2];
            var last = _valueList[_valueList.Count - 1];
            var none = _generateElement(_valueList.Count + 1).Key.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _valueList.Contains(first);
            sw.Stop();
            Console.WriteLine("First element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _valueList.Contains(middle);
            sw.Stop();
            Console.WriteLine("Middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _valueList.Contains(last);
            sw.Stop();
            Console.WriteLine("Last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _valueList.Contains(none);
            sw.Stop();
            Console.WriteLine("For the element that there is no in list: " + sw.Elapsed);
        }

        public void SearchDictionaryKeysK()
        {
            Console.WriteLine("\nKVDictionary by key\nTime of the search:\n");

            TKey first = _KVDictionary.ElementAt(0).Key;
            TKey middle = _KVDictionary.ElementAt(_KVDictionary.Count / 2).Key;
            TKey last = _KVDictionary.ElementAt(_KVDictionary.Count - 1).Key;
            TKey none = _generateElement(_KVDictionary.Count + 1).Key;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _KVDictionary.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("First element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _KVDictionary.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("Middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _KVDictionary.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("Last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            _KVDictionary.ContainsKey(none);
            sw.Stop();
            Console.WriteLine("For the element that there is no in list: " + sw.Elapsed);
        }

        public void SearchDictionaryValueV()
        {
            Console.WriteLine("\nSVDictionary by Key\nTime of the search:\n");

            var first = _SVDictionary.ElementAt(0).Key;
            var middle = _SVDictionary.ElementAt(_SVDictionary.Count / 2).Key;
            var last = _SVDictionary.ElementAt(_SVDictionary.Count - 1).Key;
            var none = _generateElement(_SVDictionary.Count + 1).Key.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _SVDictionary.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("First element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _SVDictionary.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("Middle element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _SVDictionary.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("Last element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _SVDictionary.ContainsKey(none);
            sw.Stop();
            Console.WriteLine("For the element that there is no in list: " + sw.Elapsed);
        }

        public void SearchDictionaryKeysV()
        {
            Console.WriteLine("\n_KVDictionary by Value\nTime of the search:\n");

            var first = _KVDictionary.ElementAt(0).Value;
            var middle = _KVDictionary.ElementAt(_KVDictionary.Count / 2).Value;
            var last = _KVDictionary.ElementAt(_KVDictionary.Count - 1).Value;
            var none = _generateElement(_KVDictionary.Count + 1).Value;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _KVDictionary.ContainsValue(first);
            sw.Stop();
            Console.WriteLine("First element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _KVDictionary.ContainsValue(middle);
            sw.Stop();
            Console.WriteLine("Middle element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _KVDictionary.ContainsValue(last);
            sw.Stop();
            Console.WriteLine("Last element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            _KVDictionary.ContainsValue(none);
            sw.Stop();
            Console.WriteLine("For the element that there is no in list: " + sw.Elapsed);
        }
    }
}
