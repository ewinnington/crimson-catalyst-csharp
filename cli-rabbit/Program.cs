using System;
using System.Text; 
using System.Threading.Tasks; 
using RabbitMQ.Client; 
using RabbitMQ.Client.Events;

namespace cli_rabbit
{
    class Program
    {
        static string connection = "amqp://guest:guest@localhost:5672/"; 
        public static void Publish() 
        {
            ConnectionFactory factory = new ConnectionFactory(); 
            factory.Uri = new Uri(connection);

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                    routingKey: "hello",
                                    basicProperties: null,
                                    body: body);
            }
        }

          public static int Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = factory.Uri = new Uri(connection);

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
            }

            Publish(); 

            Console.WriteLine("Running");
            Console.ReadKey();
            return 0; 
        }
    }
}
