using Catalogo.Domain.Interfaces;
using Catalogo.Infra.Data.MySQL.Context;
using System.Threading.Tasks;

namespace Catalogo.Infra.Data.MySQL.UoW
{
    public class UnitOfWorkCatalogo : IUnitOfWorkCatalogo
    {
        private readonly CatalogoContext _context;

        public UnitOfWorkCatalogo(CatalogoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}