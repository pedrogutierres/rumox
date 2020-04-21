using Core.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<bool> ExistePorCategoria(Guid categoriaId);
    }
}
