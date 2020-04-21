using Catalogo.Domain.Produtos;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Infra.Data.MySQL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Catalogo.Infra.Data.MySQL.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CatalogoContext context) : base(context)
        {
        }

        public Task<bool> ExistePorCategoria(Guid categoriaId)
        {
            return DbSet.AnyAsync(p => p.CategoriaId == categoriaId);
        }
    }
}
