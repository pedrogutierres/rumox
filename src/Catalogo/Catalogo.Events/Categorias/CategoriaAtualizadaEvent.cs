using System;

namespace Catalogo.Events.Categorias
{
    public class CategoriaAtualizadaEvent : CategoriaEvent
    {
        public CategoriaAtualizadaEvent(Guid id, string nome) : base(id, nome)
        { }
    }
}
