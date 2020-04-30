using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Usuarios
{
    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "O e-mail deve ser informado.")]
        [EmailAddress(ErrorMessage = "O e-mail está inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser informada.")]
        public string Senha { get; set; }
    }
}
