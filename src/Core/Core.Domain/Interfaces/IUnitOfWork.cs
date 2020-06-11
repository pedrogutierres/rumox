using System;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
