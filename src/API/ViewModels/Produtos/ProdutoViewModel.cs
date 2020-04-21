using System;

namespace Rumox.API.ViewModels.Produtos
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public string InformacoesAdicionais { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
