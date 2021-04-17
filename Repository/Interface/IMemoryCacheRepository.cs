using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Repository.Interface
{
    public interface IMemoryCacheRepository
    {
        void Set<T>(string key, T value) where T : class;
        T Get<T>(string key) where T : class;
        void Remove(string key);
    }
}
