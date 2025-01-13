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