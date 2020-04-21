using System;
using System.Threading.Tasks;

namespace Catalogo.Domain.Categorias.Interfaces
{
    public interface ICategoriaService
    {
        Task Registrar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Deletar(Guid id);
    }
}
