using Core.Domain.CommandHandlers;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Rumox.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // JWT
            //services.AddScoped<JwtTokenGenerate>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data EventSourcing
            //services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, TmpEventStore>();
            //services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity
            //services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IUser, AspNetUser>();

            // Infra - Filtros
            //services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            //services.AddScoped<ILogger<EmailSender>, Logger<EmailSender>>();
            //services.AddScoped<GlobalActionLogger>();

            //BootstrapperCatalogo.RegisterServices(services);
        }
    }
}