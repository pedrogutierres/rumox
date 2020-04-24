using Bogus;
using Catalogo.Domain.Categorias;
using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Domain.Categorias.Services;
using Core.Domain.Notifications;
using MediatR;
using Moq.AutoMock;
using System;
using Xunit;

namespace Catalogo.Domain.Tests.Categorias
{
    [CollectionDefinition(nameof(CategoriaCollection))]
    public class CategoriaCollection : ICollectionFixture<CategoriaTestsFixture>
    { }

    public class CategoriaTestsFixture : IDisposable
    {
        private readonly Faker _faker;
        public AutoMocker Mocker { get; private set; }

        public CategoriaTestsFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public Categoria GerarCategoriaValida()
        {
            return new Faker<Categoria>("pt_BR")
                  .CustomInstantiator(f => new Categoria(
                      Guid.NewGuid(), 
                      f.Commerce.Categories(1)[0]))
                  .Generate();
        }

        public Categoria GerarCategoriaInvalida()
        {
            return new Faker<Categoria>("pt_BR")
                 .CustomInstantiator(f => new Categoria(
                     Guid.Empty,
                     f.Lorem.Sentence(wordCount: 101)))
                 .Generate();
        }

        public ICategoriaService ObterCategoriaService()
        {
            Mocker = new AutoMocker();
            Mocker.Use<INotificationHandler<DomainNotification>>(new DomainNotificationHandler());

            return Mocker.CreateInstance<CategoriaService>();
        }

        public void Dispose()
        {
        }
    }
}
