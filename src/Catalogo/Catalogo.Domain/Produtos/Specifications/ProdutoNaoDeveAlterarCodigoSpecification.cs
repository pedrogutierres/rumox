using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;

namespace Catalogo.Domain.Produtos.Specifications
{
    public class ProdutoNaoDeveAlterarCodigoSpecification : DomainSpecification<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoNaoDeveAlterarCodigoSpecification(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;
        }

        public override bool EhValido()
        {
            var produtoOriginal = _produtoRepository.ObterPorId(Entidade.Id).Result;

            return produtoOriginal?.Codigo == Entidade.Codigo;
        }
    }
}
