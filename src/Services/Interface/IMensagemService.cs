using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IMensagemService
    {
        Mensagem MontaMensagem(string texto);
    }
}
