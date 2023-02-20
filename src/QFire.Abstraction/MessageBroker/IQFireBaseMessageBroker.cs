using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using System.Threading.Tasks;

namespace QFire.Abstraction.MessageBroker
{
    public interface IQFireBaseMessageBroker : IQFireBaseSender
    {
        Task<bool> PingAsync();
    }
}