using Catalogo.Domain.Categorias.Specifications;
using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Validations;
using FluentValidation;

namespace Catalogo.Domain.Categorias.Validations
{
    public class CategoriaAptaParaDeletarValidation : DomainValidator<Categoria>
    {
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaAptaParaDeletarValidation(Categoria entidade, IProdutoRepository produtoRepository) : base(entidade)
        {
            _produtoRepository = produtoRepository;

            ValidarSemProdutosVinculados();
        }

        private void ValidarSemProdutosVinculados()
        {
            RuleFor(p => p.Nome)
                .IsValid(new CategoriaDeveEstarSemProdutosSpecification(Entidade, _produtoRepository))
                .WithMessage("Existem produtos vinculados na categoria.");
        }
    }
}
