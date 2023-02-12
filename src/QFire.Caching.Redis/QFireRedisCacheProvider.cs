using System.Threading.Tasks;
using System;
using QFire.Abstraction.Serialization;
using QFire.Abstraction.Caching;
using QFire.Abstraction.Configuration;
using CSRedis;
using System.Collections.Generic;
using System.Linq;

namespace QFire.Caching.Redis
{
    public class QFireRedisCacheProvider : IQFireCache
    {
        private readonly IMessagePackSerializer messagePackSerializer;

        public QFireRedisCacheProvider(
            IMessagePackSerializer messagePackSerializer
            , QFireConfiguration qFireConfiguration)
        {
            this.messagePackSerializer=messagePackSerializer;
            InitCsRedis(qFireConfiguration.RedisCnnString);
        }
        public async Task<T> GetAsync<T>(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var cachedObject = await RedisHelper.GetAsync<byte[]>(cacheKey);
            if (cachedObject == null) return default(T);

            return messagePackSerializer.Deserialize<T>(cachedObject);

        }
        public async Task<bool> SetAsync(string cacheKey, object value, int expireInSecound = 86400)
        {
            if (value == null) { return false; }

            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException($"{nameof(cacheKey)}");

            if (expireInSecound <= 0)
                throw new ArgumentOutOfRangeException(nameof(expireInSecound));

            DateTime absoluteExpiration = DateTime.Now.AddSeconds(expireInSecound);

            var cacheObject = messagePackSerializer.Serialize(value);

            return await RedisHelper.SetAsync(cacheKey, cacheObject, absoluteExpiration - DateTime.Now);
        }
        public async Task<long> RemoveByKeyAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return default;
            return await RedisHelper.DelAsync(cacheKey);
        }
        public async Task<IEnumerable<string>> GetKeysByPattern(string pattern)
        {
            var keys = await RedisHelper.KeysAsync($"*{pattern}*");
            return keys.ToList();
        }
        private void InitCsRedis(string redisConnectionString)
        {
            if (string.IsNullOrWhiteSpace(redisConnectionString.Trim()))
                throw new ArgumentException(nameof(redisConnectionString));

            RedisHelper.Initialization(new CSRedisClient(redisConnectionString.Trim()));
        }
    }
}
