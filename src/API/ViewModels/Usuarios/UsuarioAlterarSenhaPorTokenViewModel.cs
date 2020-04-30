using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels
{
    public class UsuarioAlterarSenhaPorTokenViewModel
    {
        [Required(ErrorMessage = "O token deve ser informado.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "A nova senha deve ser informada.")]
        public string NovaSenha { get; set; }
    }
}