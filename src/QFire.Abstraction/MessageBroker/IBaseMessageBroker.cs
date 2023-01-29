using QFire.Abstraction.Message;

namespace QFire.Abstraction.MessageBroker
{
    public interface IBaseMessageBroker
    {
        bool Send(IQFireMessage message, Priority priority = Priority.Low);
    }
}