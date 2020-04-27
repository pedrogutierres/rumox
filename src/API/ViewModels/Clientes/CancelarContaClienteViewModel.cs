using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Clientes
{
    public class CancelarContaClienteViewModel
    {
        [Required(ErrorMessage = "A senha do cliente deve ser informada.")]
        public string Senha { get; set; }
    }
}
