using System;
using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Categorias
{
    public class RegistrarCategoriaViewModel
    {
        public Guid Id => Guid.NewGuid();

        [Required(ErrorMessage = "O nome da categoria deve ser informado.")]
        [MaxLength(100, ErrorMessage = "O nome da categoria deve conter no máximo {1} caracteres.")]
        public string Nome { get; set; }
    }
}
