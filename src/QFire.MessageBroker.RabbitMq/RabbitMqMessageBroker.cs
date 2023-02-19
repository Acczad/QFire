using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QFire.MessageBroker.RabbitMq
{
    public class RabbitMqMessageBroker : IQFireBaseMessageBroker
    {
        public Task<bool> PingAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SendAsync(QFireMessage message)
        {
            var myMessage=(TestMessage) message;
            Console.WriteLine($"message Send. duration {(DateTime.Now.Subtract(myMessage.CreateDate)).TotalSeconds} secound");
            Thread.Sleep(1000);
            return Task.FromResult(true);
        }
    }
}