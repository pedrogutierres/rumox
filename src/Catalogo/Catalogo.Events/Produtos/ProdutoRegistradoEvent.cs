using Core.Domain.Events;
using System;

namespace Catalogo.Events.Produtos
{
    public class ProdutoRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid CategoriaId { get; private set; }
        public long Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesAdicionais { get; private set; }
        public bool Ativo { get; private set; }

        public ProdutoRegistradoEvent(Guid id, Guid categoriaId, long codigo, string descricao, string informacoesAdicionais, bool ativo)
        {
            AggregateId = id;
            Id = id;
            CategoriaId = categoriaId;
            Codigo = codigo;
            Descricao = descricao;
            InformacoesAdicionais = informacoesAdicionais;
            Ativo = ativo;
        }
    }
}
