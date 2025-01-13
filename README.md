# Development
-----------

open docker
in cmd run:
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
(you can see the image in docker desktop)
(you can see the running RabbitMQ instance at localhost:8080)
When you run the RabbitMQ Docker container with the rabbitmq:3-management image, it comes preconfigured with default credentials. 
username: guest
password: guest



# Source
------

IAmTimCorey - Intro To RabbitMQ
https://www.youtube.com/watch?v=bfVddTJNiAw
=======
# ToyRabbitMQ
Experiment with RabbitMQ.

## Dev history
1. Setup a RabbitMQ server with Docker
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
2. Create the SenderApp (.NET Core Console App)
3. Create the ReceiverApp1 (.NET Core Console App)
4. Create the ReceiverApp2 (.NET Core Console App)
5. Add RabbitMQ.Client nuget package to all applications
6. Add code and run all 3 applications (with the RabbitMQ Docker container running.) 

### Sources
- [IAmTimCorey / Intro To RabbitMQ](https://youtu.be/bfVddTJNiAw)
