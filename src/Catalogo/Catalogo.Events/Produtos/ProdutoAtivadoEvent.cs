using Core.Domain.Events;
using System;

namespace Catalogo.Events.Produtos
{
    public class ProdutoAtivadoEvent : Event
    {
        public Guid Id { get; private set; }
        public bool Ativo { get; private set; } = true;

        public ProdutoAtivadoEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
