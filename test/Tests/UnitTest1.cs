using Services;
using Services.Interface;
using System;
using Xunit;

namespace Tests
{
    public class TesteMensagem
    {
        private readonly IMensagemService _mensagemService = new MensagemService();

        [Fact]
        public void Testa_Mensagem_Valida()
        {
            var textoMensagem = "Teste1";

            var mensagem = _mensagemService.MontaMensagem(textoMensagem);
            
            Assert.NotNull(mensagem);
            Assert.Equal(mensagem.Texto, textoMensagem);
        }
    }
}
