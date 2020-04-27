using System;
using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Clientes
{
    public class RegistrarClienteViewModel
    {
        public Guid Id => Guid.NewGuid();

        [Required(ErrorMessage = "O CPF do cliente deve ser informado.")]
        [MaxLength(11, ErrorMessage = "O CPF do cliente deve conter no máximo {1} caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O nome do cliente deve ser informado.")]
        [MaxLength(50, ErrorMessage = "O nome do cliente deve conter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome do cliente deve ser informado.")]
        [MaxLength(100, ErrorMessage = "O sobrenome do cliente deve conter no máximo {1} caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente deve ser informado.")]
        [EmailAddress(ErrorMessage = "O e-mail do cliente está inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha do cliente deve ser informada.")]
        public string Senha { get; set; }
    }
}
