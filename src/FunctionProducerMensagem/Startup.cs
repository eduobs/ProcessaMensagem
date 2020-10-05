using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;

[assembly: FunctionsStartup(typeof(FunctionsMenssage.Startup))]

namespace FunctionsMenssage
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IMensagemService>((s) => {
                return new MensagemService();
            });

            builder.Services.AddScoped<IBrokerService>((s) => {
                return new BrokerService();
            });
        }
    }
}
