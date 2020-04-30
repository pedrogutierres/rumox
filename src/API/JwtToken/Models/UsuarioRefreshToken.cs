using System;

namespace Rumox.API.JwtToken.Models
{
    public class UsuarioRefreshToken
    {
        public string RefreshToken { get; private set; }
        public Guid ClienteId { get; private set; }

        public UsuarioRefreshToken(string refreshToken, Guid clienteId)
        {
            RefreshToken = refreshToken;
            ClienteId = clienteId;
        }
    }
}
