using Core.Domain.Events;
using System;

namespace CRM.Events.Clientes
{
    public class ClienteEmailAlteradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }

        public ClienteEmailAlteradoEvent(Guid id, string email)
        {
            AggregateId = id;
            Id = id;
            Email = email;
        }
    }
}
