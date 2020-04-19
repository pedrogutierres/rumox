using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Domain.Interfaces
{
    public interface IUser
    {
        string Nome { get; }
        Guid UsuarioId();
        bool Autenticado();
        IEnumerable<Claim> ObterClaims();
    }
}
    