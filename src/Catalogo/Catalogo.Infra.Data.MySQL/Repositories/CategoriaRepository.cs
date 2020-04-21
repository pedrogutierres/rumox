using Catalogo.Domain.Categorias;
using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Infra.Data.MySQL.Context;

namespace Catalogo.Infra.Data.MySQL.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoContext context) : base(context)
        {
        }
    }
}
