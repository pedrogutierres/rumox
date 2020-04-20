using Core.Domain.Events;
using Core.Domain.Interfaces;

namespace Rumox.API.Configurations
{
    // TODO: Temporario até ser criado corretamente o event store
    public class TmpEventStore : IEventStore
    {
        public void SalvarEvento<T>(T evento) where T : Event
        {
            
        }
    }
}
