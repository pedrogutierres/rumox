using Bogus;
using Bogus.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Rumox.API.ViewModels.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumox.API.Tests.Catalogo.Fixtures
{
    public class CategoriaTestsFixture : IDisposable
    {
        private readonly Faker _faker;

        // Foi criado uma lista de nomes de categorias para não correr o risco de repetir o mesmo nome
        private Dictionary<string, bool> NomesCategorias { get; }

        public CategoriaTestsFixture()
        {
            _faker = new Faker("pt_BR");

            NomesCategorias = _faker.Commerce.Categories(100).GroupBy(p => p).ToDictionary(k => k.Key, v => false);
        }

        public object GerarRegistrarCategoriaViewModel()
        {
            var nomeCategoria = NomesCategorias.FirstOrDefault(p => !p.Value).Key;
            NomesCategorias[nomeCategoria] = true;

            return new Faker<object>("pt_BR")
                .CustomInstantiator(f => new
                {
                    nome = nomeCategoria
                }).Generate();
        }

        public object GerarAlterarCategoriaViewModel()
        {
            var nomeCategoria = NomesCategorias.FirstOrDefault(p => !p.Value).Key;
            NomesCategorias[nomeCategoria] = true;

            return new Faker<object>("pt_BR")
                .CustomInstantiator(f => new
                {
                    nome = nomeCategoria
                }).Generate();
        }

        public JSchema ObterSchemaCategoriaViewModel()
        {
            return JSchema.Parse(JsonConvert.SerializeObject(GerarCategoriaViewModel()));
        }

        public CategoriaViewModel GerarCategoriaViewModel()
        {
            return new Faker<CategoriaViewModel>("pt_BR")
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Nome, f => f.Commerce.Categories(1)[0])
                .RuleFor(p => p.DataHoraCriacao, f => f.Date.Recent(1))
                .RuleFor(p => p.DataHoraAlteracao, f => f.Date.Recent(1).OrNull(f))
                .Generate();
        }

        public void Dispose()
        { }
    }
}
