using Catalogo.Domain.Categorias.Interfaces;
using Core.Domain.Validations;
using System.Linq;

namespace Catalogo.Domain.Categorias.Specifications
{
    public class CategoriaDeveTerNomeUnicoSpecification : DomainSpecification<Categoria>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaDeveTerNomeUnicoSpecification(
            Categoria entidade, 
            ICategoriaRepository categoriaRepository) 
            : base(entidade)
        {
            _categoriaRepository = categoriaRepository;
        }

        public override bool EhValido()
        {
            return !_categoriaRepository.Buscar(p => p.Id != Entidade.Id && p.Nome == Entidade.Nome).Any();
        }
    }
}
