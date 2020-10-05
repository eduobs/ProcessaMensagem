using RabbitMQ.Client;
using Services.Interface;
using Services.Model;
using System;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class BrokerService : IBrokerService
    {
        private string uri;
        private string queue;

        public void EnviaMensagem(Mensagem mensagem)
        {
            uri = Environment.GetEnvironmentVariable("brokerRabbitMQ", EnvironmentVariableTarget.Process);
            queue = Environment.GetEnvironmentVariable("queue", EnvironmentVariableTarget.Process);

            var factory = new ConnectionFactory() { Uri = new Uri(uri) };

            string JsonMessage = JsonSerializer.Serialize(mensagem);

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
