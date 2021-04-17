using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using LibreriaAdmin.Repository.Interface;
using Microsoft.Extensions.Caching.Distributed;

namespace LibreriaAdmin.Repository
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IDistributedCache _iDistributedCache;

        public MemoryCacheRepository(IDistributedCache iDistributedCache)
        {

            _iDistributedCache = iDistributedCache;
        }
        public T Get<T>(string key) where T : class
        {
            return ByteArrayToObject<T>(_iDistributedCache.Get(key));
        }

        public void Remove(string key)
        {
            _iDistributedCache.Remove(key);
        }

        public void Set<T>(string key, T value) where T : class
        {
            _iDistributedCache.Set(key, ObjectToByteArray(value), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2)
            });

        }

        private byte[] ObjectToByteArray(object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        private T ByteArrayToObject<T>(byte[] bytes) where T : class
        {
            if (bytes is null) return null;
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var obj = binaryFormatter.Deserialize(memoryStream);
                return (T)obj;
            }
        }

    }
}
