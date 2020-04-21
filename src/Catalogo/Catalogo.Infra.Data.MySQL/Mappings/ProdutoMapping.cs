using Catalogo.Domain.Produtos;
using Core.Infra.MySQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infra.Data.MySQL.Mappings
{
    public class ProdutoMapping : EntityTypeConfiguration<Produto>
    {
        public override void Map(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.CategoriaId)
               .IsRequired();

            builder.Property(p => p.Codigo)
               .HasColumnType("bigint")
               .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(p => p.InformacoesAdicionais)
                .HasColumnType("text")
                .IsRequired();

            builder.IgnoreFluentValidation();

            builder.HasKey(p => p.Id);

            builder.ToTable("Produtos");
        }
    }
}
