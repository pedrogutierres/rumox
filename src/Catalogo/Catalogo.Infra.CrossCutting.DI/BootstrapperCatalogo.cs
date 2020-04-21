
using Catalogo.Domain.Categorias.Events;
using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Domain.Categorias.Services;
using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Produtos.Events;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Domain.Produtos.Services;
using Catalogo.Events.Categorias;
using Catalogo.Events.Produtos;
using Catalogo.Infra.Data.MySQL.Context;
using Catalogo.Infra.Data.MySQL.Repositories;
using Catalogo.Infra.Data.MySQL.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.Infra.CrossCutting.DI
{
    public static class BootstrapperCatalogo
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain - Services
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<CategoriaRegistradaEvent>, CategoriaEventHandler>();
            services.AddScoped<INotificationHandler<CategoriaAtualizadaEvent>, CategoriaEventHandler>();
            services.AddScoped<INotificationHandler<CategoriaDeletadaEvent>, CategoriaEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoRegistradoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoAtualizadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoAtivadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoInativadoEvent>, ProdutoEventHandler>();

            // Infra - Data
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUnitOfWorkCatalogo, UnitOfWorkCatalogo>();
            services.AddScoped<CatalogoContext>();
        }
    }
}