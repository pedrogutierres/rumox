using Bogus;
using Catalogo.Domain.Produtos;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Domain.Produtos.Services;
using Core.Domain.Notifications;
using MediatR;
using Moq.AutoMock;
using System;
using Xunit;

namespace Catalogo.Domain.Tests.Produtos
{
    [CollectionDefinition(nameof(ProdutoCollection))]
    public class ProdutoCollection : ICollectionFixture<ProdutoTestsFixture>
    { }

    public class ProdutoTestsFixture : IDisposable
    {
        private readonly Faker _faker;
        public AutoMocker Mocker { get; private set; }

        public ProdutoTestsFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public Produto GerarProdutoValido()
        {
            return new Faker<Produto>("pt_BR")
                  .CustomInstantiator(f => new Produto(
                      Guid.NewGuid(), 
                      Guid.NewGuid(),
                      Convert.ToInt64(f.Commerce.Ean13()),
                      f.Commerce.Product(),
                      f.Commerce.ProductMaterial()))
                  .Generate();
        }

        public Produto GerarProdutoInvalido()
        {
            return new Faker<Produto>("pt_BR")
                 .CustomInstantiator(f => new Produto(
                     Guid.Empty,
                     Guid.Empty,
                     0,
                     f.Lorem.Sentence(wordCount: 201),
                     string.Empty))
                 .Generate();
        }

        public Produto GerarProdutoInativo()
        {
            return new Faker<Produto>("pt_BR")
                  .CustomInstantiator(f => new Produto(
                      Guid.NewGuid(),
                      Guid.NewGuid(),
                      Convert.ToInt64(f.Commerce.Ean13()),
                      f.Commerce.Product(),
                      f.Commerce.ProductMaterial()))
                  .RuleFor(p => p.Ativo, p => false)
                  .Generate();
        }

        public IProdutoService ObterProdutoService()
        {
            Mocker = new AutoMocker();
            Mocker.Use<INotificationHandler<DomainNotification>>(new DomainNotificationHandler());

            return Mocker.CreateInstance<ProdutoService>();
        }

        public void Dispose()
        {
        }
    }
}
