using System;
using System.Collections.Generic;
using System.Text;

namespace Rumox.API.Tests.Config
{
    public class UsuarioLogado
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public string Senha { get; private set; }

        public UsuarioLogado(Guid id, string email, string accessToken, string refreshToken, string senha)
        {
            Id = id;
            Email = email;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Senha = senha;
        }

        public void AtualizarDados(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
