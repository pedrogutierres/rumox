using Xunit;

namespace CRM.Domain.Tests.Clientes
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Instanciar cliente válido.")]
        [Trait("Cliente", "Domínio")]
        public void Cliente_Instanciar_Valido()
        {
            var cliente = _clienteTestsFixture.GerarClienteValido();
            Assert.True(cliente.EhValido());
        }

        [Fact(DisplayName = "Instanciar cliente inválido.")]
        [Trait("Cliente", "Domínio")]
        public void Cliente_Instanciar_Invalida()
        {
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
            Assert.False(cliente.EhValido());
        }

        [Fact(DisplayName = "Alterar dados do cliente com sucesso.")]
        [Trait("Cliente", "Domínio")]
        public void Cliente_AlterarNome_ComSucesso()
        {
            var cliente = _clienteTestsFixture.GerarClienteValido();

            var novoNome = "Novonome";
            var novoSobrenome = "Novosobrenome";
            var novoEmail = "novoemail@email.com";
            cliente.AlterarDados(novoNome, novoSobrenome);
            cliente.AlterarEmail(novoEmail);

            Assert.Equal(novoNome, cliente.Nome);
            Assert.Equal(novoSobrenome, cliente.Sobrenome);
            Assert.Equal(novoEmail, cliente.Email);
        }

        [Fact(DisplayName = "Cancelar conta do cliente com sucesso.")]
        [Trait("Cliente", "Domínio")]
        public void Cliente_Inativar_ComSucesso()
        {
            var cliente = _clienteTestsFixture.GerarClienteInativo();
            cliente.CancelarConta();
            Assert.False(cliente.Ativo);
        }
    }
}
