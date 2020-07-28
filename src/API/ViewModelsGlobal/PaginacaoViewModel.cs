using System.Collections.Generic;

namespace Rumox.API.ViewModelsGlobal
{
    public class PaginacaoViewModel<T>
    {
        public IEnumerable<T> Itens { get; set; }
        public PaginacaoMetadataViewModel Metadata { get; set; }

        public static PaginacaoViewModel<T> NovaPaginacao(IEnumerable<T> itens, int totalItens, int totalPages)
        {
            return new PaginacaoViewModel<T>
            {
                Itens = itens,
                Metadata = new PaginacaoMetadataViewModel(totalItens, totalPages)
            };
        }
    }

    public class PaginacaoMetadataViewModel
    {
        public int TotalItens { get; set; }
        public int TotalPages { get; set; }

        public PaginacaoMetadataViewModel(int totalItens, int totalPages)
        {
            TotalItens = totalItens;
            TotalPages = totalPages;
        }
    }
}
