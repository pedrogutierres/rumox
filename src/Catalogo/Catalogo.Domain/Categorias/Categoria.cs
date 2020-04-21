using Catalogo.Domain.Categorias.Validations;
using Core.Domain.Models;
using System;

namespace Catalogo.Domain.Categorias
{
    public class Categoria : Entity<Categoria>
    {
        public string Nome { get; private set; }
        public DateTime DataHoraCriacao { get; private set; } = DateTime.Now;
        public DateTime? DataHoraAlteracao { get; private set; }

        private Categoria() { }
        public Categoria(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AlteraNome(string nome)
        {
            Nome = nome;
        }

        public override bool EhValido()
        {
            ValidationResult = new CategoriaEstaConsistenteValidation(this).Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
