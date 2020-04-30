using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels
{
    public class UsuarioRecuperarSenhaViewModel
    {
        [Required(ErrorMessage = "O e-mail deve ser informado.")]
        public string Email { get; set; }
    }
}