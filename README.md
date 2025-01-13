# ToyRabbitMQ
Experiment with RabbitMQ.

## Info
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
- ```5672``` the port for sending queue messages to the server (internally it's ```5672``` and this command maps it externally to the same)<br>
- ```15672``` the webserver is normally on this port but now mapped to ```8080```

## To Run
1. Open Docker
2. Setup a RabbitMQ server with Docker, run in CMD:
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
3. (The RabbitMQ image will be visible in Docker Desktop)
4. (You can access the RabbitMQ instance at http://localhost:8080/#/ with credentials: guest/guest)*
5. Run all 3 applications. (Setup app is already configured for all 3)<br>
   Although, you don't have to run them all at once. If the RabbitMQ service is running, the SenderApp can put messages on the queue, be stopped and a ReceiverApp started to pull messages from the queue.<br>
   You can watch ready-unacked-total messages at ```http://localhost:8080/#/queues```.

\* When you run the RabbitMQ Docker container with the rabbitmq:3-management image, it comes preconfigured with default credentials.

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

## Sources
- [IAmTimCorey / Intro To RabbitMQ](https://youtu.be/bfVddTJNiAw)
