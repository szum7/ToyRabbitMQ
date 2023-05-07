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
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");

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