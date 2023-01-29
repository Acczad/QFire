using QFire.Abstraction.Message;

namespace QFire.Abstraction.Core
{
    public interface IQFire
    {
        void Produce(IQFireMessage message, Priority isHighPriority = Priority.Low);
    }
}