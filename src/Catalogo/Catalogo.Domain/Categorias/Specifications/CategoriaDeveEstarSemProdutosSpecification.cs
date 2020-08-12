using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;
using System.Threading.Tasks;

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

        public override async Task<bool> EhValido()
        {
            return !await _produtoRepository.ExistePorCategoria(Entidade.Id);
        }
    }
}
