using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiverApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ReceiverApp2 START.");

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factory.ClientProvidedName = "MyRabbitMQ ReceiverApp2";

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
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
            };

            var consumerTag = channel.BasicConsume(queueName, false, consumer);

            Console.ReadLine();

            channel.BasicCancel(consumerTag);

            channel.Close();
            connection.Close();

            Console.WriteLine("ReceiverApp2 END.");
        }
    }
}