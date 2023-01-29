using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;

namespace QFire.Core.Queue
{
    public class QFire<T> : IQFire<T> where T : QFireMessage
    {
        private readonly IQFireRepository<T> _messageRepository;

        public QFire(IQFireRepository<T> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public bool Send(T message)
        {
            return _messageRepository.EnQueueMessage(message);
        }
    }
}