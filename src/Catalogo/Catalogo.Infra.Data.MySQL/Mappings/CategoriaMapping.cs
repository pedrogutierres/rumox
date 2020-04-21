using Catalogo.Domain.Categorias;
using Core.Infra.MySQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infra.Data.MySQL.Mappings
{
    public class CategoriaMapping : EntityTypeConfiguration<Categoria>
    {
        public override void Map(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.IgnoreFluentValidation();

            builder.HasKey(p => p.Id);

            builder.ToTable("Categorias");
        }
    }
}
