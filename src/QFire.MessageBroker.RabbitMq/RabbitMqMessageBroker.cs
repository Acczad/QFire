using Newtonsoft.Json;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using QFire.MessageBroker.RabbitMq.Contract;
using System.Text;
using System.Threading.Tasks;

namespace QFire.MessageBroker.RabbitMq
{
    public class RabbitMqMessageBroker : IQFireBaseMessageBroker
    {
        private readonly IQFireRabbitMqFactory qFireRabbitMqFactory;

        public RabbitMqMessageBroker(IQFireRabbitMqFactory qFireRabbitMqFactory)
        {
            this.qFireRabbitMqFactory=qFireRabbitMqFactory;
        }
        public Task<bool> PingAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SendAsync(QFireMessage message)
        {
            var channel = qFireRabbitMqFactory.GetChannel();
            var queueName = message.GetQueueName();

            channel.QueueDeclareNoWait(queueName, false, false, false, null);

            var payload = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(payload);

            channel.BasicPublish("", queueName, false, null, body);
            return Task.FromResult(true);
        }
    }
}