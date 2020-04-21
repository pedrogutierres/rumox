using Catalogo.Domain.Produtos.Interface;
using Catalogo.Domain.Produtos.Specifications;
using Core.Domain.Validations;
using FluentValidation;

namespace Catalogo.Domain.Produtos.Validations
{
    public class ProdutoAptoParaRegistrarValidation : DomainValidator<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAptoParaRegistrarValidation(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;

            ValidarCodigo();
        }

        private void ValidarCodigo()
        {
            RuleFor(p => p.Codigo)
                .IsValid(new ProdutoDeveTerCodigoUnicoSpecification(Entidade, _produtoRepository))
                .WithMessage("Já existe outro produto com o código informado.");
        }
    }
}
