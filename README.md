![Logo of the project](./src/icon.png)

# QFire &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/acczad/qfire/blob/main/LICENSE.txt)
> Additional information or tag line

A producer-consumer pattern for sending messages to MessageBroker.
According to the pattern of the producer and consumer of this package, they send information in two phases.
First, a message is sent by the user and it is placed in a queue inside the memory or Redis (based on the importance of the message), then a series of consumption services are automatically created and send messages depending on the capacity of the consumer's server.
Its advantage is that the process of sending messages is very simple and fast, and if there is a problem for the Consumer server, the messages will not be lost, and after the Consumer server is re-established, the messages will be sent again.
Each user can have their own implementation for sending method according to the contract.
There is a sample project where you can see how to use this service.

## Installing / Getting started

A quick introduction of the minimal setup you need to get a hello world up &
running.

```shell
commands here
```

Here you should say what actually happens when you execute the code above.

## Developing

### Built With
List main libraries, frameworks used including versions (React, Angular etc...)

### Prerequisites
What is needed to set up the dev environment. For instance, global dependencies or any other tools. include download links.



## Versioning

We can maybe use [SemVer](http://semver.org/) for versioning. For the versions available, see the [link to tags on this repository](/tags).


## Configuration

Here you should write what are all of the configurations a user can enter when
using the project.


## Style guide

Explain your code style and show how to check it.
