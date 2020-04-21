using System;
using System.ComponentModel.DataAnnotations;

namespace Rumox.API.ViewModels.Produtos
{
    public class RegistrarProdutoViewModel
    {
        public Guid Id => Guid.NewGuid();

        [Required(ErrorMessage = "A categoria do produto deve ser informada.")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O código do produto deve ser informado.")]
        public long Codigo { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informada.")]
        [MaxLength(200, ErrorMessage = "A descrição do produto deve conter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "As informações adicionais do produto devem ser informadas.")]
        public string InformacoesAdicionais { get; set; }
    }
}
