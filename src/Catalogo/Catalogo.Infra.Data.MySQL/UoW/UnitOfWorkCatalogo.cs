using Catalogo.Domain.Interfaces;
using Catalogo.Infra.Data.MySQL.Context;

namespace Catalogo.Infra.Data.MySQL.UoW
{
    public class UnitOfWorkCatalogo : IUnitOfWorkCatalogo
    {
        private readonly CatalogoContext _context;

        public UnitOfWorkCatalogo(CatalogoContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}