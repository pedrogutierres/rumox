using Core.Domain.Validations;
using FluentValidation;

namespace Catalogo.Domain.Produtos.Validations
{
    public class ProdutoEstaConsistenteValidation : DomainValidator<Produto>
    {
        public ProdutoEstaConsistenteValidation(Produto entidade) : base(entidade)
        {
            ValidarCategoria();
            ValidarCodigo();
            ValidarDescricao();
            ValidarInformacoesAdicionais();
        }

        public void ValidarCategoria()
        {
            RuleFor(p => p.CategoriaId)
                .NotEmpty().WithMessage("A categoria do produto deve ser informada.");
        }

        public void ValidarCodigo()
        {
            RuleFor(p => p.Codigo)
                .GreaterThan(0).WithMessage("O código do produto deve ser maior que {ComparisonValue}.");
        }

        public void ValidarDescricao()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("A descrição do produto deve ser informada.")
                .MaximumLength(200).WithMessage("A descrição do produto deve conter no máximo {MaxLength} caracteres.");
        }

        public void ValidarInformacoesAdicionais()
        {
            RuleFor(p => p.InformacoesAdicionais)
                .NotEmpty().WithMessage("As informações adicionais do produto devem ser informadas.");
        }
    }
}
