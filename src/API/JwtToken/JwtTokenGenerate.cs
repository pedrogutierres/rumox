using Core.Cache;
using Core.Infra.CrossCutting.Identity.Authorization;
using CRM.Domain.Clientes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Rumox.API.Configurations;
using Rumox.API.JwtToken.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rumox.API.JwtToken
{
    public class JwtTokenGenerate
    {
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IDistributedCache _cache;

        private static string CacheKeyRefreshToken(string token) => $"cliente:refreshtoken:{token}";

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public JwtTokenGenerate(
            TokenDescriptor tokenDescriptor,
            IDistributedCache cache)
        {
            _tokenDescriptor = tokenDescriptor;
            _cache = cache;
        }

        public async Task<AuthToken> GerarToken(Cliente cliente)
        {
            return await GerarToken(new UsuarioCliente(cliente.Id, $"{cliente.Nome} {cliente.Sobrenome}", cliente.Email), DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid));
        }

        private async Task<AuthToken> GerarToken(UsuarioBase usuario, DateTime dataExpiracao)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            switch (usuario)
            {
                case UsuarioCliente _:
                    identityClaims.AddClaims(AuthorizationUsers.RetornarClaimsUsuario());
                    break;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.UtcNow, // Considerar possível diferença de data e hora entre servidores
                Expires = dataExpiracao
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedJwt = tokenHandler.WriteToken(token);

            var refreshToken = await GerarRefreshTokenAsync(usuario.Id);

            return new AuthToken
            {
                result = new AuthResult
                {
                    access_token = encodedJwt,
                    expires_in = dataExpiracao,
                    refresh_token = refreshToken,
                    user = new AuthUser
                    {
                        id = usuario.Id,
                        nomeCompleto = usuario.NomeCompleto,
                        email = usuario.Email,
                        claims = identityClaims.Claims.Select(c => new AuthClaim { type = c.Type, value = c.Value })
                    }
                }
            };
        }

        private async Task<string> GerarRefreshTokenAsync(Guid usuarioId)
        {
            var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            var refreshTokenData = new UsuarioRefreshToken(refreshToken, usuarioId);

            var cacheKey = CacheKeyRefreshToken(refreshToken);

            await _cache.CreateOrUpdateAsync(cacheKey, refreshTokenData, minutesExpiration: _tokenDescriptor.RefreshTokenMinutes);

            return refreshToken;
        }

        public async Task ValidarRefreshTokenAsync(Guid usuarioId, string refreshToken)
        {
            var cacheKey = CacheKeyRefreshToken(refreshToken);

            string strTokenArmazenado = await _cache.GetStringAsync(cacheKey);
            if (string.IsNullOrWhiteSpace(strTokenArmazenado))
                throw new RefreshTokenException("Refresh Token expirado.");

            var refreshTokenBase = JsonConvert.DeserializeObject<UsuarioRefreshToken>(strTokenArmazenado);
            if (refreshTokenBase == null)
                throw new RefreshTokenException("Refresh Token inválido.");
            
            if (usuarioId != refreshTokenBase.ClienteId || refreshToken != refreshTokenBase.RefreshToken)
                throw new RefreshTokenException("Refresh Token incorreto.");

            await _cache.RemoveAsync(cacheKey);
        }
    }

    internal class RefreshTokenException : Exception
    {
        public RefreshTokenException(string message)
            : base(message)
        { }
    }
}