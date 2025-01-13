using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiverApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ReceiverApp1 START.");

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factory.ClientProvidedName = "MyRabbitMQ ReceiverApp1";

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            var exchangeName = "DemoExchange";
            var routingKey = "demo-routing-key";
            var queueName = "DemoQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                // By grabbing the message, the message will be gone if we acknowledge it at the end.
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // This is where we do something with the message.
                // Send it to the database, or call some method, do some work on it, etc.
                Console.WriteLine($"Message received: {message}");

                // We can set it to not acknowledge it if we have for example an error/exception/etc
                channel.BasicAck(args.DeliveryTag, false);

                // Delay
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            };

            var consumerTag = channel.BasicConsume(queueName, false, consumer);

            Console.ReadLine();

            channel.BasicCancel(consumerTag);

            channel.Close();
            connection.Close();

            Console.WriteLine("ReceiverApp1 END.");
        }
    }
}