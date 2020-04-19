using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Infra.CrossCutting.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        // TODO: Salvar este dado em um local mais seguro e alterá-lo
        private const string SecretKey = "rumox@SecretKey";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}