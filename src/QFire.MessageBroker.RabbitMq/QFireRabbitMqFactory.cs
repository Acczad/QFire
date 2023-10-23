using QFire.Abstraction.Configuration;
using RabbitMQ.Client;
using System;

namespace QFire.MessageBroker.RabbitMq.Contract
{
    public class QFireRabbitMqFactory : IQFireRabbitMqFactory
    {
        private readonly string userName;
        private readonly string password;
        private readonly string host;
        private readonly string port;

        private IConnection _connection;
        private IModel _chanel;

        public QFireRabbitMqFactory(QFireConfiguration qFireConfiguration)
        {
            this.userName=qFireConfiguration.RabbitMqUserName;
            this.password=qFireConfiguration.RabbitMqPassword;
            this.host=qFireConfiguration.RabbitMqHost;
            this.port=qFireConfiguration.RabbitMqPort;
        }

        private IConnection GetConnection()
        {
            if (_connection is { IsOpen: true }) return _connection;

            var _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(
                    $"amqp://{this.userName}:{this.password}@{this.host}:{this.port}")
            };

            _connection = _connectionFactory.CreateConnection();
            return _connection;
        }

        public IModel GetChannel()
        {
            if (_chanel == null || _chanel.IsClosed)
            {
                _chanel = GetConnection().CreateModel();
            }

            return _chanel;
        }
    }
}
