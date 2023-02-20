using RabbitMQ.Client;

namespace QFire.MessageBroker.RabbitMq.Contract
{
    public interface IQFireRabbitMqFactory
    {
        IModel GetChannel();
    }
}
