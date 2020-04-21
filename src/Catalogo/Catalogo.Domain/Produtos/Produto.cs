using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Domain.Produtos
{
    public class Produto : Entity<Produto>
    {
        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
