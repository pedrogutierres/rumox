using System;

namespace Rumox.API.JwtToken.Models
{
    internal abstract class UsuarioBase
    { 
        public Guid Id { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public string Email { get; protected set; }

        protected UsuarioBase(Guid id, string nomeCompleto, string email)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Email = email;
        }
    }
}
