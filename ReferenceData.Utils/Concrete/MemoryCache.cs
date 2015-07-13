using ReferenceData.Utils.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Utils.Concrete
{
    public class MemoryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private Dictionary<TKey, TValue> storageDictionary = new Dictionary<TKey, TValue>();

        public void Put(TKey key, TValue value)
        {
            storageDictionary[key] = value;
        }

        public void PutAll(IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var value in values)
                storageDictionary[value.Key] = value.Value;
        }

        public TValue Get(TKey key)
        {
            return storageDictionary[key];
        }

        public IEnumerable<TValue> GetAll()
        {
            return storageDictionary.Values;
        }

        public bool Contains(TKey key)
        {
            return storageDictionary.ContainsKey(key);
        }
    }
}
