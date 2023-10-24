![Logo of the project](./icon.png)

# QFire &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/acczad/qfire/blob/main/LICENSE.txt) [![NuGet version (QFire)](https://img.shields.io/nuget/v/QFire.svg?style=flat-square)]([https://www.nuget.org/packages/QFire])
> A .NET library library for queuing messages.
<br />
This is the .NET library for creating in memory queue and send it to external sources,
Based on producer and consumer pattern This library send messages in two phases.<br /><br />

**First:** <br />
A message is sent by the user, then the message is placed in a queue on the memory. (If the sent message is important, a backup copy can also be stored on Redis).

**Second:<br />**
Depending on the needs and settings, a group of worker threads will start receiving and sending messages. This system ensures that the message is sent correctly and in case of an error, the message is returned to the queue.
Since the main stack is a **Thread-Safe** queue, multiple threads can simultaneously add or remove something from it.

**Third:<br />**
If the sending result is not successful, the message will be transferred to the queue again.<br />

## Installing / Getting started
Install nuget package :
> NuGet\Install-Package QFire <br />

config QFire with this command :

```c#
        services
            .ConfigureQFire<TestMessage>(typeof(RabbitMqMessageBroker), // your implementation of message sender 
                option => option.WithRedis(     //use redis as message backup database
                option =>
                option.SetMaxWorkerThread(10)         // maximum worker threads work on message stack
                .SetCacheAbbrivation("tst")           // cache abbrivation by multiple projects
                .SetMaxDeleyInWorkerCreation(4)       // dealy between worker threads creations
                .SetMaxWorkerRetryBeforeTerminate(3)  // retey count before worker thread termination
                .SetMaxMessageCacheTime(24*36000)     // maximum message backup time in secound
                .SetRedisCnnString("127.0.0.1")       // Redis connection string
                .SetRabbitMqHost("127.0.0.1")         // RabbitMq connection string
                .SetRabbitMqPassword("password")      // RabbitMq password
                .SetRabbitMqUserName("username")      // RabbitMq username
                ));
```

then you have to create your message 
```c#
        [MessagePackObject(keyAsPropertyName: true)]  // MessagePack attribute for serialization in redis
        public class TestMessage : QFireMessage        
        {
            public TestMessage(Priority priority = Priority.Low) : base(priority) // High Priority messsage will be backed up in Redis
             {
               CreateDate= DateTime.Now;
             }

            public string MyMessage { get; set; }
            public DateTime CreateDate { get; set; }
        }
```
send message :

```c#
 private readonly IQFire<TestMessage> qFire;

 var message = new TestMessage { MyMessage= "test message" ) };
 message.SetPriority(Priority.High);
 await qFire.SendAsync(message);

```
**How message sender works:** <br />
Qfire use **IQFireBaseMessageBroker** interface for sending messages
and in ConfigureQFire method you can pass your sender.<br />

As an example : <br />
```c#
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
```
## Developing
**You can fork this repository and add cool feautures**
