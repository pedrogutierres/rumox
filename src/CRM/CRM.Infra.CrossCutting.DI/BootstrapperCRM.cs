using CRM.Domain.Clientes.Events;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.Services;
using CRM.Domain.Interfaces;
using CRM.Events.Clientes;
using CRM.Infra.Data.Mongo.Context;
using CRM.Infra.Data.Mongo.Repositories;
using CRM.Infra.Data.Mongo.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Infra.CrossCutting.DI
{
    public static class BootstrapperCRM
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain - Services
            services.AddScoped<IClienteService, ClienteService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteAtualizadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteEmailAlteradoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteContaCanceladaEvent>, ClienteEventHandler>();

            // Infra - Data
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUnitOfWorkCRM, UnitOfWorkCRM>();
            services.AddScoped<CRMContext>();
        }
    }
}