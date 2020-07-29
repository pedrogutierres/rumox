using Core.Domain.Events;
using System;

namespace Catalogo.Events.Produtos
{
    public sealed class ProdutoAtualizadoEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesAdicionais { get; private set; }

        public ProdutoAtualizadoEvent(Guid id, Guid categoriaId, string descricao, string informacoesAdicionais)
        {
            AggregateId = id;
            Id = id;
            CategoriaId = categoriaId;
            Descricao = descricao;
            InformacoesAdicionais = informacoesAdicionais;
        }
    }
}
