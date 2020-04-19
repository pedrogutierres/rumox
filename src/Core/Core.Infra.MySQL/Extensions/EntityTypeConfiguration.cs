using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infra.MySQL.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
