using Core.Domain.Events;
using System;

namespace Catalogo.Events.Produtos
{
    public sealed class ProdutoInativadoEvent : Event
    {
        public Guid Id { get; private set; }
        public bool Ativo { get; private set; } = false;

        public ProdutoInativadoEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
