using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Utils.Abstract
{
    public interface ICache<TKey, TValue>
    {
        void Put(TKey key, TValue value);
        void PutAll(IEnumerable<KeyValuePair<TKey, TValue>> values);
        TValue Get(TKey key);
        IEnumerable<TValue> GetAll();
        bool Contains(TKey key);
    }
}
