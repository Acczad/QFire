using QFire.Abstraction.Message;

namespace QFire.Abstraction.MessageBroker
{
    public interface IBaseMessageBroker
    {
        bool Send(QFireMessage message);
    }
}