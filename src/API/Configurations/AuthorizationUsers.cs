using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace Rumox.API.Configurations
{
    public static class AuthorizationUsers
    {
        public static void AddPolicysUsers(this AuthorizationOptions options)
        {
            /*options.AddPolicy("PolicyExemplo", policy => policy.RequireAssertion(context =>
                context.User.IsInRole("Usuario") ||
                context.User.IsInRole("Admin") ||
                context.User.HasClaim(c => c.Type == "ConsultarTest")));*/
        }

        public static IEnumerable<Claim> RetornarClaimsUsuario()
        {
            var claims = new Collection<Claim>();
            claims.Add(new Claim("role", "Usuario"));
            //claims.Add(new Claim("ConsultarTest", "Gravar"));
            return claims;
        }

        public static IEnumerable<Claim> RetornarClaimsAdmin()
        {
            var claims = new Collection<Claim>();
            claims.Add(new Claim("role", "Admin"));
            return claims;
        }
    }
}