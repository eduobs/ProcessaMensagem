using Services.Model;

namespace Services.Interface
{
    public interface IBrokerService
    {
        void EnviaMensagem(Mensagem mensagem);
    }
}
