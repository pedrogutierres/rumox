using Catalogo.Infra.Data.MySQL.Mappings;
using Core.Infra.MySQL;
using Core.Infra.MySQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Catalogo.Infra.Data.MySQL.Context
{
    public class CatalogoContext : MySQLContext
    {
        public CatalogoContext(IConfiguration configuration, ILoggerFactory loggerFactory)
            : base(configuration, loggerFactory)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CategoriaMapping());
            modelBuilder.AddConfiguration(new ProdutoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}