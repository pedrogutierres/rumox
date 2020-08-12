using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Specifications
{
    public class ProdutoDeveTerCodigoUnicoSpecification : DomainSpecification<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoDeveTerCodigoUnicoSpecification(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;
        }

        public override async Task<bool> EhValido()
        {
            return await Task.FromResult(!_produtoRepository.Buscar(p => p.Id != Entidade.Id && p.Codigo == Entidade.Codigo).Any());
        }
    }
}
