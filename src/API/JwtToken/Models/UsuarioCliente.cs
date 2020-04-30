using System;

namespace Rumox.API.JwtToken.Models
{
    internal class UsuarioCliente : UsuarioBase
    {
        public UsuarioCliente(Guid id, string nomeCompleto, string email)
            : base(id, nomeCompleto, email)
        {

        }
    }
}
