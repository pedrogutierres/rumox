using Catalogo.Domain.Produtos.Interface;
using Catalogo.Domain.Produtos.Specifications;
using Core.Domain.Validations;
using FluentValidation;

namespace Catalogo.Domain.Produtos.Validations
{
    public class ProdutoAptoParaAtualizarValidation : DomainValidator<Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAptoParaAtualizarValidation(Produto entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;

            ValidarCodigo();
        }

        private void ValidarCodigo()
        {
            RuleFor(p => p.Codigo)
                .IsValid(new ProdutoNaoDeveAlterarCodigoSpecification(Entidade, _produtoRepository))
                .WithMessage("O produto não deve ter seu código alterado.");
        }
    }
}
