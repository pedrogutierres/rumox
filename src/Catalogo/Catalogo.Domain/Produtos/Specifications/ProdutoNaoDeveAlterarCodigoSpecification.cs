using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Specifications
{
    public class ProdutoNaoDeveAlterarCodigoSpecification : DomainSpecification<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoNaoDeveAlterarCodigoSpecification(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;
        }

        public override async Task<bool> EhValido()
        {
            var produtoOriginal = await _produtoRepository.ObterPorId(Entidade.Id);

            return produtoOriginal?.Codigo == Entidade.Codigo;
        }
    }
}
