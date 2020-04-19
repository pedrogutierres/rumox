using Core.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infra.MySQL.Extensions
{
    public static class EntityTypeExtensions
    {
        public static void IgnoreFluentValidation<TEntity>(this EntityTypeBuilder<TEntity> modelBuilder)
            where TEntity : Entity<TEntity>
        {
            modelBuilder.Ignore(p => p.ValidationResult);
        }

        public static void IgnoreDataHoraCriacaoEAlteracao<TEntity>(this EntityTypeBuilder<TEntity> modelBuilder)
            where TEntity : Entity<TEntity>
        {
            modelBuilder.Ignore(p => p.DataHoraCriacao);
            modelBuilder.Ignore(p => p.DataHoraAlteracao);
        }
    }
}
