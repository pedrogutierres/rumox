using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Produtos
{
    public class AlterarSituacaoProdutoViewModel
    {
        [Required(ErrorMessage = "A situação do produto deve ser informada.")]
        public bool Ativo { get; set; } = true;
    }
}
