using Core.Domain.Events;
using System;

namespace CRM.Events.Clientes
{
    public sealed class ClienteAtualizadoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }

        public ClienteAtualizadoEvent(Guid id, string nome, string sobrenome)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
        }
    }
}
