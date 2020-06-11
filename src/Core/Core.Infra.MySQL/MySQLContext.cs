using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infra.MySQL
{
    public abstract class MySQLContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILoggerFactory _loggerFactory;

        public MySQLContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Convert.ToBoolean(_configuration.GetSection("Logging")?["LogarDatabase"] ?? "false"))
                optionsBuilder.UseLoggerFactory(_loggerFactory);

            optionsBuilder.UseMySql(_configuration.GetMySQLDbConnectionString());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(p => p.Entity.GetType().GetProperty("DataHoraCriacao") != null
                                                                 || p.Entity.GetType().GetProperty("DataHoraAlteracao") != null))
            {
                if (entry.Properties.Any(p => p.Metadata.Name == "DataHoraCriacao"))
                {
                    var dataHoraCriacao = entry.Property("DataHoraCriacao");

                    if (entry.State == EntityState.Added && (dataHoraCriacao.CurrentValue == null || DateTime.MinValue.Equals(dataHoraCriacao.CurrentValue)))
                        dataHoraCriacao.CurrentValue = DateTime.Now;
                    else if (entry.State == EntityState.Modified)
                        dataHoraCriacao.IsModified = false;
                }

                if (entry.Properties.Any(p => p.Metadata.Name == "DataHoraAlteracao"))
                {
                    var dataHoraAlteracao = entry.Property("DataHoraAlteracao");

                    if (entry.State == EntityState.Modified && (dataHoraAlteracao.CurrentValue == null || DateTime.MinValue.Equals(dataHoraAlteracao.CurrentValue)))
                    {
                        dataHoraAlteracao.CurrentValue = DateTime.Now;
                        dataHoraAlteracao.IsModified = true;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
