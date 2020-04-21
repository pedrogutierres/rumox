using System;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Interface
{
    public interface IProdutoService
    {
        Task Registrar(Produto produto);
        Task Atualizar(Produto produto);
        Task Ativar(Guid id);
        Task Inativar(Guid id);
    }
}
