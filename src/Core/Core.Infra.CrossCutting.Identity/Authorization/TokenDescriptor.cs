namespace Core.Infra.CrossCutting.Identity.Authorization
{
    public class TokenDescriptor
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int MinutesValid { get; set; }
        public int RefreshTokenMinutes { get; set; }
    }
}