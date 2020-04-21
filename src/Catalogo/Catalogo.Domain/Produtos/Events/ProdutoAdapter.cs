using Catalogo.Events.Produtos;
using System;

namespace Catalogo.Domain.Produtos.Events
{
    internal static class ProdutoAdapter
    {
        public static ProdutoRegistradoEvent ToProdutoRegistradoEvent(Produto produto)
        {
            return new ProdutoRegistradoEvent(produto.Id, produto.CategoriaId, produto.Codigo, produto.Descricao, produto.InformacoesAdicionais, produto.Ativo);
        }

        public static ProdutoAtualizadoEvent ToProdutoAtualizadoEvent(Produto produto)
        {
            return new ProdutoAtualizadoEvent(produto.Id, produto.CategoriaId, produto.Descricao, produto.InformacoesAdicionais);
        }

        public static ProdutoAtivadoEvent ToProdutoAtivadoEvent(Guid id)
        {
            return new ProdutoAtivadoEvent(id);
        }

        public static ProdutoInativadoEvent ToProdutoInativadoEvent(Guid id)
        {
            return new ProdutoInativadoEvent(id);
        }
    }
}
