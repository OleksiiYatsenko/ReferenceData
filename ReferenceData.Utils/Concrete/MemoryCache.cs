using ReferenceData.Utils.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Utils.Concrete
{
    public class MemoryCache<T> : ICache<string, T>
    {
        private readonly System.Runtime.Caching.MemoryCache storage;
        
        private const int DEFAULT_MEMORY_LIMIT = 10000;
        private const int DEFAULT_PHYSICAL_MEMORY_LIMIT_PERCENTAGE = 0;
        private const int DEFAULT_POOLING_MINUTES = 10;

        public MemoryCache() : this(DEFAULT_MEMORY_LIMIT, DEFAULT_PHYSICAL_MEMORY_LIMIT_PERCENTAGE, TimeSpan.FromMinutes(DEFAULT_POOLING_MINUTES)) { }

        public MemoryCache(int memoryLimit) : this(memoryLimit, DEFAULT_PHYSICAL_MEMORY_LIMIT_PERCENTAGE, TimeSpan.FromSeconds(DEFAULT_POOLING_MINUTES)) { }

        public MemoryCache(int memoryLimit, int physicalMemoryLimitPercentage, TimeSpan pollingInterval)
        {
            var cacheConfig = new NameValueCollection();
            cacheConfig.Add("cacheMemoryLimitMegabytes", memoryLimit.ToString());
            cacheConfig.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercentage.ToString());
            cacheConfig.Add("pollingInterval", pollingInterval.ToString());
            storage = new System.Runtime.Caching.MemoryCache(typeof(T).ToString(), cacheConfig);
        }

        public void Put(string key, T value, DateTimeOffset? expirationDate)
        {
            var date = expirationDate != null ? (DateTimeOffset)expirationDate : DateTimeOffset.MaxValue;
            storage.Set(key, value, date);
        }

        public void PutAll(IEnumerable<KeyValuePair<string, T>> values, DateTimeOffset? expirationDate)
        {
            var date = expirationDate != null ? (DateTimeOffset)expirationDate : DateTimeOffset.MaxValue;
            foreach (var value in values)
                Put(value.Key, value.Value, date);
        }

        public bool TryGet(string key, out T value)
        {
            if (!storage.Contains(key))
            {
                value = default(T);
                return false;
            }

            value = (T)storage.Get(key);
            return true;
        }

        public T Get(string key)
        {
            T result;
            if (!TryGet(key, out result))
                throw new KeyNotFoundException(string.Format("There are no cache entry with key: {0}", key));

            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return storage.Select(cacheEntry => (T)cacheEntry.Value);
        }

        public bool Contains(string key)
        {
            return storage.Contains(key);
        }
    }
}
