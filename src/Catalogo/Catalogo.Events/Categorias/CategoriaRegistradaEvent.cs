using System;

namespace Catalogo.Events.Categorias
{
    public class CategoriaRegistradaEvent : CategoriaEvent
    {
        public CategoriaRegistradaEvent(Guid id, string nome) : base(id, nome)
        { }
    }
}
