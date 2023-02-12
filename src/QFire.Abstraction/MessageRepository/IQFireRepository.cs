using QFire.Abstraction.Message;
using System.Threading.Tasks;

namespace QFire.Abstraction.MessageRepository
{
    public interface IQFireRepository<T> where T : QFireMessage
    {
        Task<bool> EnQueueMessageAsync(T message);
        Task<T> DeQueueMessageAsync();
        int GetQueueCount();
        Task FinalizeMessageAsync(T message, bool sendStatus);
    }
}