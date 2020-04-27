using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Clientes
{
    public class AlterarEmailClienteViewModel
    {
        [Required(ErrorMessage = "O e-mail do cliente deve ser informado.")]
        [EmailAddress(ErrorMessage = "O e-mail do cliente está inválido.")]
        public string Email { get; set; }
    }
}
