using QFire.Abstraction.Message;

namespace QFire.Abstraction.MessageRepository
{
    public interface IQFireInMemoryRepository<T> : IQFireRepository<T> where T : QFireMessage
    {
    }
}