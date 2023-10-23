![Logo of the project](./icon.png)

# QFire &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/acczad/qfire/blob/main/LICENSE.txt)
> A reliable and robust library for queuing messages.

This is the .NET library for creating in memory queue and send it to external sources.
Based on producer and consumer pattern This library send messages in two phases.
First:
A message is sent by the user, then the message is placed in a queue on the memory. (If the sent message is important, a backup copy can also be stored on Redis).
Second:
Depending on the needs and settings, a group of Threads will start receiving and sending messages. This system ensures that the message is sent correctly and in case of an error, the message is returned to the queue.
Since the main stack is a **Thread-safe** queue, multiple threads can simultaneously add or remove something from it.
Third:
If the sending result is not successful, the message will be transferred to the queue again.

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
