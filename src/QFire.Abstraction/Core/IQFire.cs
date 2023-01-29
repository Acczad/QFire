using QFire.Abstraction.Message;

namespace QFire.Abstraction.Core
{
    public interface IQFire<T> where T : QFireMessage
    {
        bool Send(T message);
    }
}