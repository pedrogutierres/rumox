using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace Core.Infra.Log.ELK.Services
{
    internal static class ServiceLogExtensions
    {
        internal static void RegisterLogService(this IServiceCollection services, IConfiguration configuration)
        {
            // Utilizando o Try para não adicionar multiplas vezes o mesmo service
            services.TryAddSingleton(sp =>
            {
                var factory = new ConnectionFactory();
                configuration.Bind("Logging:EnterpriseLog:RabbitMQ", factory);
                return factory;
            });

            services.TryAddSingleton<IConnection>(sp => sp.GetRequiredService<ConnectionFactory>().CreateConnection());
            services.TryAddTransient<IModel>(sp => sp.GetRequiredService<IConnection>().CreateModel());

            services.TryAddTransient<DispatchService>();
        }
    }
}
