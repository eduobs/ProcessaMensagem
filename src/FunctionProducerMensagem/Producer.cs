using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using Services.Interface;
using System;
using System.Text;
using System.Text.Json;

namespace FunctionMenssage
{
    public class Producer
    {
        private readonly IMensagemService _mensagemService;
        private readonly IBrokerService _brokerService;

        public Producer(IMensagemService mensagemService, IBrokerService brokerService)
        {
            _mensagemService = mensagemService;
            _brokerService = brokerService;
        }

        [FunctionName("Producer")]
        public void Run([TimerTrigger("*/5 * * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"Vou realizar o envio de uma nova mensagem em: {DateTime.Now}");

            try
            {
                var mensagem = _mensagemService.MontaMensagem("Hello Word");
                _brokerService.EnviaMensagem(mensagem);
                log.LogInformation($"Mensagem enviada com sucesso");
            }
            catch (Exception ex)
            {
                log.LogError($"Erro no envio de mensagem para o broker: {ex.Message}");
            }
        }
    }
}
