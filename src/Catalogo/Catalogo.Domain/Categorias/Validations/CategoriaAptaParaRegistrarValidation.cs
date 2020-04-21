using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Domain.Categorias.Specifications;
using Core.Domain.Validations;
using FluentValidation;

namespace Catalogo.Domain.Categorias.Validations
{
    public class CategoriaAptaParaRegistrarValidation : DomainValidator<Categoria>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaAptaParaRegistrarValidation(Categoria entidade, ICategoriaRepository categoriaRepository) : base(entidade)
        {
            _categoriaRepository = categoriaRepository;

            ValidarNomeUnico();
        }

        private void ValidarNomeUnico()
        {
            RuleFor(p => p.Nome)
                .IsValid(new CategoriaDeveTerNomeUnicoSpecification(Entidade, _categoriaRepository))
                .WithMessage("O nome da categoria deve ser único.");
        }
    }
}
