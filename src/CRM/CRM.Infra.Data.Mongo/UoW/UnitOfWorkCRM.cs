using CRM.Domain.Interfaces;
using CRM.Infra.Data.Mongo.Context;
using System.Threading.Tasks;

namespace CRM.Infra.Data.Mongo.UoW
{
    public class UnitOfWorkCRM : IUnitOfWorkCRM
    {
        private readonly CRMContext _context;

        public UnitOfWorkCRM(CRMContext context)
        {
            _context = context;
        }

        public Task<bool> Commit()
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}