using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace rabbitmq
{
    class Program
    {
        static void Main(string[] args)
        {
            //Envio();

            var factory = new ConnectionFactory() {  
                HostName = "reindeer.rmq.cloudamqp.com",
                VirtualHost = "lhnpiovn",
                UserName = "lhnpiovn",
                Password = "i3MAZJQ4lqoHrQkk9YsXbEzUiLt4wrH7"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine($" {ea.RoutingKey} [x] Received {message}");
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer
                                     );

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }


        static void Envio()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "reindeer.rmq.cloudamqp.com",
                VirtualHost = "lhnpiovn",
                UserName = "lhnpiovn",
                Password = "i3MAZJQ4lqoHrQkk9YsXbEzUiLt4wrH7"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
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
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }
    }
}
