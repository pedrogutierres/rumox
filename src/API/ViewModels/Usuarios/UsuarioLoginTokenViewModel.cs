using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels
{
    public class UsuarioLoginTokenViewModel
    {
        [Required(ErrorMessage = "O token precisa ser informado.")]
        public string RefreshToken { get; set; }
    }
}