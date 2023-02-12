using System.Collections.Generic;
using System.Threading.Tasks;

namespace QFire.Abstraction.Caching
{
    public interface IQFireCache
    {
        Task<T> GetAsync<T>(string cacheKey);
        Task<bool> SetAsync(string cacheKey, object value, int expireInSecound);
        Task<long> RemoveByKeyAsync(string cacheKey);
        Task<IEnumerable<string>> GetKeysByPattern(string pattern);
    }
}
