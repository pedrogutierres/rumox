using Bogus;
using Bogus.Extensions.Brazil;
using Core.Domain.Notifications;
using Core.Domain.ValueObjects;
using CRM.Domain.Clientes;
using CRM.Domain.Clientes.Interfaces;
using CRM.Domain.Clientes.Services;
using CRM.Domain.Clientes.ValuesObjects;
using MediatR;
using Moq.AutoMock;
using System;
using Xunit;

namespace CRM.Domain.Tests.Clientes
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    { }

    public class ClienteTestsFixture : IDisposable
    {
        private readonly Faker _faker;
        public AutoMocker Mocker { get; private set; }

        public ClienteTestsFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public Cliente GerarClienteValido()
        {
            var dataHoraCadastro = DateTime.Now;
            var person = _faker.Person;

            return new Faker<Cliente>("pt_BR")
                  .CustomInstantiator(f => new Cliente(
                      Guid.NewGuid(),
                      new CPF(person.Cpf(false)),
                      person.FirstName,
                      person.LastName,
                      person.Email,
                      dataHoraCadastro,
                      ClienteSenha.Factory.NovaSenha(f.Random.AlphaNumeric(10), dataHoraCadastro)))
                  .Generate();
        }

        public Cliente GerarClienteInvalido()
        {
            return new Faker<Cliente>("pt_BR")
                 .CustomInstantiator(f => new Cliente(
                     Guid.Empty,
                     null,
                     f.Lorem.Sentence(wordCount: 51),
                     f.Lorem.Sentence(wordCount: 101),
                     string.Empty,
                     DateTime.MinValue,
                     ClienteSenha.Factory.NovaSenha("", DateTime.Now)))
                 .Generate();
        }

        public Cliente GerarClienteInativo()
        {
            var dataHoraCadastro = DateTime.Now;
            var person = _faker.Person;

            return new Faker<Cliente>("pt_BR")
                  .CustomInstantiator(f => new Cliente(
                      Guid.NewGuid(),
                      new CPF(person.Cpf(false)),
                      person.FirstName,
                      person.LastName,
                      person.Email,
                      dataHoraCadastro,
                      ClienteSenha.Factory.NovaSenha(f.Random.AlphaNumeric(10), dataHoraCadastro)))
                  .RuleFor(p => p.Ativo, p => false)
                  .Generate();
        }

        public IClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();
            Mocker.Use<INotificationHandler<DomainNotification>>(new DomainNotificationHandler());

            return Mocker.CreateInstance<ClienteService>();
        }

        public void Dispose()
        {
        }
    }
}
