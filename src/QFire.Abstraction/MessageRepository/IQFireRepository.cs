using System.Collections.Concurrent;
using System.Collections.Generic;
using QFire.Abstraction.Message;

namespace QFire.Abstraction.MessageRepository
{
    public interface IQFireRepository<T> where T : QFireMessage
    {
        bool EnQueueMessage(T message);
        T DeQueueMessage();
        
    }
}