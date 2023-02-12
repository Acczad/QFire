using QFire.Abstraction.Message;
using System.Threading.Tasks;

namespace QFire.Abstraction.MessageBroker
{
    public interface IBaseMessageBroker
    {
        Task<bool> SendAsync(QFireMessage message);
    }
}