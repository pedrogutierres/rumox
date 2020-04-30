using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels
{
    public class UsuarioAlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual deve ser informada.")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "A nova senha deve ser informada.")]
        public string NovaSenha { get; set; }
    }
}