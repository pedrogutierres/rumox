using System;

namespace Catalogo.Events.Categorias
{
    public sealed class CategoriaAtualizadaEvent : CategoriaEvent
    {
        public CategoriaAtualizadaEvent(Guid id, string nome) : base(id, nome)
        { }
    }
}
