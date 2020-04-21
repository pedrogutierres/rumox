using Core.Domain.Events;
using System;

namespace Catalogo.Events.Categorias
{
    public abstract class CategoriaEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }

        public CategoriaEvent(Guid id, string nome)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
        }
    }
}
