using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;
using System.Linq;

namespace Catalogo.Domain.Produtos.Specifications
{
    public class ProdutoDeveTerCodigoUnicoSpecification : DomainSpecification<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoDeveTerCodigoUnicoSpecification(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;
        }

        public override bool EhValido()
        {
            return !_produtoRepository.Buscar(p => p.Id != Entidade.Id && p.Codigo == Entidade.Codigo).Result.Any();
        }
    }
}
