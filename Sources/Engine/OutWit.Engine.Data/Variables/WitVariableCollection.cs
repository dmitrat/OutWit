using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Data.Variables
{
    public class WitVariableCollection : IEnumerable<IWitVariable>
    {
        #region Constructors

        public WitVariableCollection()
        {
            Items = new ConcurrentDictionary<string, IWitVariable>();
        }

        public WitVariableCollection(IEnumerable<KeyValuePair<string, IWitVariable>> variables)
        {
            Items = new ConcurrentDictionary<string, IWitVariable>(variables);
        } 

        #endregion

        #region Functions

        public void Add(IWitVariable variable)
        {
            Items.TryAdd(variable.Name, variable);
        }

        public WitVariableCollection Join(IEnumerable<IWitVariable> variables)
        {
            var collection = new WitVariableCollection(Items);

            foreach (var variable in variables)
                collection.Add(variable);
            
            return collection;
        }

        public bool Contains(string variableKey)
        {
            return Items.ContainsKey(variableKey);
        }

        #endregion

        #region GetValue

        public string GetString(string key)
        {
            if (key.ToUpper() == "NULL")
                return "";

            if (Contains(key))
                return $"{this[key].Value}";

            return key;
        }

        public int GetInt(string key)
        {
            if (key.ToUpper() == "NULL")
                return 0;

            if (Contains(key))
                return (int)this[key].Value;

            return int.Parse(key);
        }

        public double GetDouble(string key)
        {
            if (key.ToUpper() == "NULL")
                return 0.0;

            if (Contains(key))
                return (double)this[key].Value;

            return double.Parse(key);
        }

        #endregion

        #region IEnumerable

        public IEnumerator< IWitVariable> GetEnumerator()
        {
            return Items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Properties
        
        private ConcurrentDictionary<string, IWitVariable> Items { get;}

        public IWitVariable this[string key] => Items[key];

        public int Count => Items.Count; 

        #endregion
    }
}
