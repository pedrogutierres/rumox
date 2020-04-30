using System;
using System.Collections.Generic;

namespace Rumox.API.JwtToken
{
    public class AuthToken
    {
        public AuthResult result { get; set; }
    }

    public class AuthResult
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime expires_in { get; set; }
        public AuthUser user { get; set; }
    }

    public class AuthClaim
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class AuthUser
    {
        public Guid id { get; set; }
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public IEnumerable<AuthClaim> claims { get; set; } = new List<AuthClaim>();
    }
}