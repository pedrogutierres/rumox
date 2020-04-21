using Catalogo.Events.Categorias;

namespace Catalogo.Domain.Categorias.Events
{
    internal static class CategoriaAdapter
    {
        public static CategoriaRegistradaEvent ToCategoriaRegistradaEvent(Categoria categoria)
        {
            return new CategoriaRegistradaEvent(categoria.Id, categoria.Nome);
        }

        public static CategoriaAtualizadaEvent ToCategoriaAtualizadaEvent(Categoria categoria)
        {
            return new CategoriaAtualizadaEvent(categoria.Id, categoria.Nome);
        }

        public static CategoriaDeletadaEvent ToCategoriaDeletadaEvent(Categoria categoria)
        {
            return new CategoriaDeletadaEvent(categoria.Id, categoria.Nome);
        }
    }
}
