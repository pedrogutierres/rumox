using Core.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Domain.Categorias.Validations
{
    public class CategoriaEstaConsistenteValidation : DomainValidator<Categoria>
    {
        public CategoriaEstaConsistenteValidation(Categoria entidade) : base(entidade)
        {
            ValidarNome();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado.");
        }
    }
}
