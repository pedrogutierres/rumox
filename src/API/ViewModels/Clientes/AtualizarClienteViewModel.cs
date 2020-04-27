using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Clientes
{
    public class AtualizarClienteViewModel
    {
        [Required(ErrorMessage = "O nome do cliente deve ser informado.")]
        [MaxLength(50, ErrorMessage = "O nome do cliente deve conter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome do cliente deve ser informado.")]
        [MaxLength(100, ErrorMessage = "O sobrenome do cliente deve conter no máximo {1} caracteres.")]
        public string Sobrenome { get; set; }
    }
}
