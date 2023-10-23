using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;

namespace QFire.MessageRepository
{
    public class QFireInMemoryMessageRepository<T> : IQFireInMemoryRepository<T> where T : QFireMessage
    {
        private readonly IMessageKeyGenerator messageKeyGenerator;
        public QFireInMemoryMessageRepository(IMessageKeyGenerator messageKeyGenerator)
        {
            Queue = new BlockingCollection<QFireMessage>(new ConcurrentQueue<QFireMessage>());
            this.messageKeyGenerator=messageKeyGenerator;
        }
        public BlockingCollection<QFireMessage> Queue { get; }
        public Task<bool> EnQueueMessageAsync(T message)
        {
            var key = messageKeyGenerator.GenerateKey();
            message.SetId(key);
            Queue.Add(message);
            return Task.FromResult(true);
        }
        public Task<T> DeQueueMessageAsync()
        {
            QFireMessage resultMessage;
            Queue.TryTake(out resultMessage, TimeSpan.FromMilliseconds(1500));
            return Task.FromResult((T)resultMessage);
        }
        public int GetQueueCount()
        {
            return Queue.Count;
        }
        public async Task FinalizeMessageAsync(T message, bool sendStatus)
        {
            if (sendStatus) return;
            await EnQueueMessageAsync(message);
        }
    }
}