using Core.Domain.Interfaces;
using System.Threading.Tasks;

namespace Core.Domain.Defaults
{
    public class UnitOfWorkDefault : IUnitOfWork
    {
        public Task<bool> Commit()
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        { }
    }
}
