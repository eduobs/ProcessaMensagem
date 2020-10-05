using Services.Interface;
using Services.Model;
using System;

namespace Services
{
    public class MensagemService : IMensagemService
    {
        public Mensagem MontaMensagem(string texto)
        {
            return new Mensagem()
            {
                Texto = texto,
                IdentificadorServico = Environment.GetEnvironmentVariable("IdentificadorDoServico", EnvironmentVariableTarget.Process),
                IdUnico = Guid.NewGuid(),
                Time5tamp = GetTimestamp(DateTime.Now)
            };
        }

        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
