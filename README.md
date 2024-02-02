![Logo of the project](./icon.png)

# QFire &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/acczad/qfire/blob/main/LICENSE.txt) [![NuGet version (QFire)](https://img.shields.io/nuget/v/QFire.svg?style=flat-square)]([https://www.nuget.org/packages/QFire])
> QFire is the .NET library for creating in job-queuing and send it to external sources, based on producer and consumer pattern.
<br />
The purpose of this library is to manage queues efficiently in situations involving a high volume of messages, such as sending emails, text messages, or saving logs. It serves as a mechanism for queue management, particularly useful when using message brokers is not feasible. The library helps control message loads, keeping unnecessary messages in memory and sending them based on consumer ability.

Even more, if Redis is used as a queue, the producer and consumer can be completely separated from each other and several consumers can be used in the form of microservices. But this library can be used in any desired way.
<br />
In this library, queuing operations are performed in the following steps:

<br />

1) A message is created by the user, then the message is placed in a queue on the memory, If the sent message is important stored on Redis. 
(Logs may not be valuable messages that can be queued on memory. By accepting the risk that this information may be lost, it causes less I/O operations to occur and helps the system's efficiency.)
<br />

2) Depending on the load and settings, a group of worker threads will start receiving and consuming messages. This system ensures that the message is consume correctly and in case of an error, the message is returned to the queue. If the queue is empty, the worker threads are automatically shut down and start working when the queue is refilled. The number of these worker threads can be adjusted in the initial settings.
One of the advantages of this library is that you design the implementation and the logic you have to consume the message yourself. This library is only responsible for breaking the load for you.

<br />

3) If the sending result is not successful, the message will be transferred to the queue again.
<br />

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
## Purpose / Benefit
In scenarios where we work with a large volume of messages, we need a mechanism for queue management. for example
Send emails
Send text messages
Save logs
Sometimes it is not possible to use message brokers like Rabbit MQ, so you can use this library to manage high load of messages,
and also this library can control the volume of the load sent to Message Broker and keep unnecessary messages on the memory and send them according to the conditions.
## Developing
**You can fork this repository and add cool feautures**
