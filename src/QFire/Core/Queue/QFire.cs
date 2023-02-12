using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;
using System.Threading.Tasks;

namespace QFire.Core.Queue
{
    public class QFire<T> : IQFire<T> where T : QFireMessage
    {
        private readonly IQFireRepository<T> _messageRepository;

        public QFire(IQFireRepository<T> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<bool> SendAsync(T message)
        {
            return await _messageRepository.EnQueueMessageAsync(message);
        }
    }
}