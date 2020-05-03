using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Rumox.API.ViewModels.Clientes;
using System;
using System.Runtime.Serialization;

namespace Rumox.API.Tests.CRM.Fixtures
{
    public class ClienteTestsFixture : IDisposable
    {
        public readonly Faker Faker;
        private (string, string, string) ClienteParaCancelarConta; // (id, email, senha)

        public ClienteTestsFixture()
        {
            Faker = new Faker("pt_BR");
        }

        public object GerarRegistrarClienteViewModel(string senha = null)
        {
            var person = new Faker("pt_BR").Person;

            return new Faker<object>("pt_BR")
                .CustomInstantiator(f => new
                {
                    cpf = person.Cpf(false),
                    nome = person.FirstName,
                    sobrenome = person.LastName,
                    email = person.Email,
                    senha = (senha ?? f.Random.AlphaNumeric(10))
                }).Generate();
        }

        public object GerarAtualizarClienteViewModel()
        {
            var person = new Faker("pt_BR").Person;

            return new Faker<object>("pt_BR")
                 .CustomInstantiator(f => new
                 {
                     nome = person.FirstName,
                     sobrenome = person.LastName,
                 }).Generate();
        }

        public object GerarAlterarEmailClienteViewModel()
        {
            var person = new Faker("pt_BR").Person;

            return new Faker<object>("pt_BR")
                 .CustomInstantiator(f => new
                 {
                     email = person.Email
                 }).Generate();
        }

        public void RegistrarClienteParaCancelar(string id, string email, string senha)
        {
            ClienteParaCancelarConta = (id, email, senha);
        }
        public object GerarCancelarContaClienteViewModel(out string id, out string email)
        {
            id = ClienteParaCancelarConta.Item1;
            email = ClienteParaCancelarConta.Item2;

            return new Faker<object>("pt_BR")
                 .CustomInstantiator(f => new
                 {
                     senha = ClienteParaCancelarConta.Item3
                 }).Generate();
        }

        public JSchema ObterSchemaClienteViewModel()
        {
            return JSchema.Parse(JsonConvert.SerializeObject(GerarClienteViewModel()));
        }

        public ClienteViewModel GerarClienteViewModel()
        {
            var person = Faker.Person;

            return new Faker<ClienteViewModel>("pt_BR")
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.CPF, f => person.Cpf(false))
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Sobrenome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Email, f => f.Commerce.ProductName())
                .RuleFor(p => p.Ativo, f => true)
                .RuleFor(p => p.DataHoraCriacao, f => f.Date.Recent(1))
                .RuleFor(p => p.DataHoraAlteracao, f => f.Date.Recent(1).OrNull(f))
                .Generate();
        }

        public void Dispose()
        { }
    }
}
