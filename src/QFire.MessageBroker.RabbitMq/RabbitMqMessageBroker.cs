using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker.RabbitMq;

namespace QFire.MessageBroker.RabbitMq
{
    public class RabbitMqMessageBroker : IRabbitMqMessageBroker
    {
        public bool Send(QFireMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}