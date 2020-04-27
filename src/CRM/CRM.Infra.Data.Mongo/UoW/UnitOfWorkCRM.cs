using CRM.Domain.Interfaces;
using CRM.Infra.Data.Mongo.Context;

namespace CRM.Infra.Data.Mongo.UoW
{
    public class UnitOfWorkCRM : IUnitOfWorkCRM
    {
        private readonly CRMContext _context;

        public UnitOfWorkCRM(CRMContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}