using System;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FunctionMenssage
{
    public static class Producer
    {
        [FunctionName("Producer")]
        public static void Run([TimerTrigger("*/5 * * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"Vou realizar o envio de uma nova mensagem em: {DateTime.Now}");

            var mensagem = new Mensagem()
            {
                Texto = "Hello Word",
                IdentificadorServico = Environment.GetEnvironmentVariable("IdentificadorDoServico", EnvironmentVariableTarget.Process),
                IdUnico = Guid.NewGuid(),
                Time5tamp = GetTimestamp(DateTime.Now)
            };


            var factory = new ConnectionFactory() { Uri = new Uri("amqp://testes:RabbitMQ2020!@localhost:5672") };

            string JsonMessage = JsonSerializer.Serialize(mensagem);

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "queue-mensagem",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "queue-mensagem",
                                     basicProperties: null,
                                     body: body);
            }

            log.LogInformation($"Mensagem enviada com sucesso: {JsonMessage}");
        }

        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
