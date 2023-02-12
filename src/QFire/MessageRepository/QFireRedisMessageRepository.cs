using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using QFire.Abstraction.Caching;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;

namespace QFire.MessageRepository
{
    public class QFireRedisMessageRepository<T> : IQFireInRedisRepository<T> where T : QFireMessage
    {
        private readonly IQFireCache qFireCache;
        private readonly IMessageKeyGenerator messageKeyGenerator;
        private readonly QFireConfiguration qFireConfiguration;
        public BlockingCollection<string> Queue { get; }

        public QFireRedisMessageRepository
            (
            IQFireCache qFireCache
            , IMessageKeyGenerator messageKeyGenerator
            , QFireConfiguration qFireConfiguration)
        {
            Queue = new BlockingCollection<string>(new ConcurrentQueue<string>());
            this.qFireCache=qFireCache;
            this.messageKeyGenerator=messageKeyGenerator;
            this.qFireConfiguration=qFireConfiguration;
        }

        public async Task<bool> EnQueueMessageAsync(T message)
        {
            var key = messageKeyGenerator.GenerateKey();
            message.SetId(key);
            await qFireCache.SetAsync(key, message, qFireConfiguration.MaxMessageInCache);
            Queue.Add(key);
            return true;
        }
        public async Task<T> DeQueueMessageAsync()
        {
            string cacheKey;
            Queue.TryTake(out cacheKey, TimeSpan.FromMilliseconds(1500));
            return await qFireCache.GetAsync<T>(cacheKey);
        }
        public int GetQueueCount()
        {
            return Queue.Count;
        }
        public async Task FinalizeMessageAsync(T message, bool sendStatus)
        {
            if (sendStatus)
            {
                await qFireCache.RemoveByKeyAsync(message.GetId());
                return;
            }

            Queue.Add(message.GetId());
        }
        public void LoadCachedMessageKeys(IEnumerable<string> keys)
        {
            foreach (var key in keys)
                Queue.Add(key);
        }
    }
}