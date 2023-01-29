using System;
using System.Collections.Concurrent;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;

namespace QFire.Infrastructure.MessageRepository
{
    public class QFireInMemoryRepository<T> : IQFireRepository<T> where T : QFireMessage
    {
        public QFireInMemoryRepository()
        {
            HotQueue = new BlockingCollection<T>(new ConcurrentQueue<T>());
            Queue = new BlockingCollection<T>(new ConcurrentQueue<T>());
        }

        public BlockingCollection<T> HotQueue { get; }
        public BlockingCollection<T> Queue { get; }

        public bool EnQueueMessage(T message)
        {
            if (message.Priority == Priority.High)
                HotQueue.Add(message);
            else
                Queue.Add(message);

            return true;
        }

        public T DeQueueMessage()
        {
            T resultMessage;
            if (HotQueue.Count > 0)
            {
                HotQueue.TryTake(out resultMessage, TimeSpan.FromMilliseconds(1500));
                return resultMessage;
            }

            Queue.TryTake(out resultMessage, TimeSpan.FromMilliseconds(1500));
            return resultMessage;
        }
    }
}