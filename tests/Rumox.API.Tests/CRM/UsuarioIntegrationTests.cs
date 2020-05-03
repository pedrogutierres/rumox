using Newtonsoft.Json;
using Rumox.API.JwtToken;
using Rumox.API.ResponseType;
using Rumox.API.Tests.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Rumox.API.Tests.CRM
{
    [TestCaseOrderer("Rumox.API.Tests.Config.PriorityOrderer", "Rumox.API.Tests")]
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class UsuarioIntegrationTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

        public UsuarioIntegrationTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Login de usuario com sucesso"), TestPriority(11)]
        [Trait("Usuario", "Integração")]
        public void Usuario_LoginDeCliente_Sucesso()
        {
            // Arrange
            var usuario = _testsFixture.UsuarioLogado;

            // Assert
            Assert.NotNull(usuario);
            Assert.NotNull(usuario.AccessToken);
            Assert.NotNull(usuario.RefreshToken);
        }

        [Fact(DisplayName = "Login de usuario pelo refresh token com sucesso"), TestPriority(12)]
        [Trait("Usuario", "Integração")]
        public async Task Usuario_LoginDeClientePeloRefreshToken_Sucesso()
        {
            // Arrange
            var data = new { refreshToken = _testsFixture.UsuarioLogado.RefreshToken };

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync($"usuarios/login/{_testsFixture.UsuarioLogado.Id}/refresh-token", data);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<ResponseSuccess<AuthToken>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.result);
            Assert.NotNull(result.Data.result.access_token);
            Assert.NotEmpty(result.Data.result.access_token);
        }

        [Fact(DisplayName = "Alterar senha do usuario com sucesso"), TestPriority(13)]
        [Trait("Usuario", "Integração")]
        public async Task Usuario_AlterarSenha_Sucesso()
        {
            // Arrange
            var data = new { senhaAtual = _testsFixture.UsuarioLogado.Senha, novaSenha = "SenhaAlterada@2020#" };

            // Act
            var response = await _testsFixture.Client.PatchAsJsonAsync($"usuarios/{_testsFixture.UsuarioLogado.Id}/alterar-senha", data);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
