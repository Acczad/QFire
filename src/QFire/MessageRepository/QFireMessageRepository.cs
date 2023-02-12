using QFire.Abstraction.Message;
using QFire.Abstraction.MessageRepository;
using System.Threading.Tasks;

namespace QFire.MessageRepository
{
    public class QFireMessageRepository<T> : IQFireRepository<T> where T : QFireMessage
    {
        private readonly IQFireInMemoryRepository<T> qFireInMemoryRepository;
        private readonly IQFireInRedisRepository<T> qFireInRedisRepository;

        public QFireMessageRepository(
            IQFireInMemoryRepository<T> qFireInMemoryRepository,
            IQFireInRedisRepository<T> qFireInRedisRepository)
        {
            this.qFireInMemoryRepository=qFireInMemoryRepository;
            this.qFireInRedisRepository=qFireInRedisRepository;
        }
        public async Task<bool> EnQueueMessageAsync(T message)
        {
            if (message.IsHighPriority())
                await qFireInRedisRepository.EnQueueMessageAsync(message);
            else
                await qFireInMemoryRepository.EnQueueMessageAsync(message);

            return true;
        }
        public async Task<T> DeQueueMessageAsync()
        {
            if (qFireInRedisRepository.GetQueueCount() > 0)
                return await qFireInRedisRepository.DeQueueMessageAsync();

            return await qFireInMemoryRepository.DeQueueMessageAsync();
        }
        public int GetQueueCount()
        {
            return
                qFireInRedisRepository.GetQueueCount()
                +
                qFireInMemoryRepository.GetQueueCount();
        }
        public async Task FinalizeMessageAsync(T message, bool sendStatus)
        {
            if (message.IsHighPriority())
            {
                await qFireInRedisRepository.FinalizeMessageAsync(message, sendStatus);
                return;
            }
            await qFireInMemoryRepository.FinalizeMessageAsync(message, sendStatus);
        }
    }
}