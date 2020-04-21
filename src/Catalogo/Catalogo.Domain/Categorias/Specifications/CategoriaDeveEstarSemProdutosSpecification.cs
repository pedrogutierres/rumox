using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;

namespace Catalogo.Domain.Categorias.Specifications
{
    public class CategoriaDeveEstarSemProdutosSpecification : DomainSpecification<Categoria>
    {
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaDeveEstarSemProdutosSpecification(
            Categoria entidade, 
            IProdutoRepository produtoRepository) 
            : base(entidade)
        {
            _produtoRepository = produtoRepository;
        }

        public override bool EhValido()
        {
            return !_produtoRepository.ExistePorCategoria(Entidade.Id).Result;
        }
    }
}
