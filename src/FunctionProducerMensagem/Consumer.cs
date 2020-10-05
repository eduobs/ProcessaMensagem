using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Services.Model;
using System.Text.Json;

namespace FunctionMenssage
{
    public static class Consumer
    {
        [FunctionName("Consumer")]
        public static void Run([RabbitMQTrigger("queue-mensagem", ConnectionStringSetting = "brokerRabbitMQ")]string myQueueItem, ILogger log)
        {
            var mensagem = JsonSerializer.Deserialize<Mensagem>(myQueueItem);
            log.LogInformation($"Mensagem recebida: {mensagem.Texto}, ID: {mensagem.IdUnico} do serviço: {mensagem.IdentificadorServico}");
        }
    }
}
