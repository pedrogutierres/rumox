using System;

namespace Catalogo.Events.Categorias
{
    public sealed class CategoriaDeletadaEvent : CategoriaEvent
    {
        public CategoriaDeletadaEvent(Guid id, string nome) : base(id, nome)
        { }
    }
}
