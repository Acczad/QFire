# QFire
A producer-consumer pattern for sending messages to MessageBroker.

According to the pattern of the producer and consumer of this package, they send information in two phases.
First, a message is sent by the user and it is placed in a queue inside the memory or Redis (based on the importance of the message), then a series of consumption services are automatically created and send messages depending on the capacity of the consumer's server.
Its advantage is that the process of sending messages is very simple and fast, and if there is a problem for the Consumer server, the messages will not be lost, and after the Consumer server is re-established, the messages will be sent again.
Each user can have their own implementation for sending method according to the contract.

There is a sample project where you can see how to use this service.
