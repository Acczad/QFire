using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using System.Threading;
using System.Threading.Tasks;

namespace QFire.MessageBroker.RabbitMq
{
    public class RabbitMqMessageBroker : IBaseMessageBroker
    {
        public Task<bool> SendAsync(QFireMessage message)
        {
            var myMessage=(TestMessage) message;

            Thread.Sleep(1000);
            return Task.FromResult(true);
        }
    }
}