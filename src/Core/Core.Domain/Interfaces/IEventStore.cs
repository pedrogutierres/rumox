using Core.Domain.Events;

namespace Core.Domain.Interfaces
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}