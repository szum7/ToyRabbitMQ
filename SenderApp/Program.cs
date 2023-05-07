using RabbitMQ.Client;
using System;
using System.Text;

namespace SenderApp
{
    class Program
    {
        static string GetMessage(int index)
        {
            var milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return $"Message from SenderApp (#{index}) [{milliseconds}]";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("SenderApp START.");

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factory.ClientProvidedName = "MyRabbitMQ SenderApp";

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            var exchangeName = "DemoExchange";
            var routingKey = "demo-routing-key";
            var queueName = "DemoQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Sending message #{i + 1}");
                byte[] messageBodyBytes = Encoding.UTF8.GetBytes(GetMessage(i + 1));
                channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

                // Delay
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }


            channel.Close();
            connection.Close();

            Console.WriteLine("SenderApp END.");
        }
    }
}