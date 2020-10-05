using System;

namespace Services.Model
{
    public class Mensagem
    {
        public string Texto { get; set; }
        public string IdentificadorServico { get; set; }
        public string Time5tamp { get; set; }
        public Guid IdUnico { get; set; }
    }
}
