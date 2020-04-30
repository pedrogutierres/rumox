using Core.Domain.Events;
using System;

namespace CRM.Events.Clientes
{
    public class ClienteSenhaAlteradaEvent : Event
    {
        public Guid Id { get; private set; }

        public ClienteSenhaAlteradaEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
