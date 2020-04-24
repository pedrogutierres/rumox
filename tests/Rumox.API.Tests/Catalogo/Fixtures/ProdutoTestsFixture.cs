using Bogus;
using Bogus.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Rumox.API.ViewModels.Produtos;
using System;

namespace Rumox.API.Tests.Catalogo.Fixtures
{
    public class ProdutoTestsFixture : IDisposable
    {
        public ProdutoTestsFixture()
        { }

        public object GerarRegistrarProdutoViewModel(Guid categoriaId)
        {
            return new Faker<object>("pt_BR")
                .CustomInstantiator(f => new
                {
                    categoriaId = categoriaId,
                    codigo = Convert.ToInt64(f.Commerce.Ean13()),
                    descricao = f.Commerce.Department(),
                    informacoesAdicionais = f.Commerce.ProductMaterial()
                }).Generate();
        }

        public object GerarAlterarProdutoViewModel(Guid categoriaId)
        {
            return new Faker<object>("pt_BR")
                 .CustomInstantiator(f => new
                 {
                     categoriaId = categoriaId,
                     descricao = f.Commerce.Department(),
                     informacoesAdicionais = f.Commerce.ProductMaterial()
                 }).Generate();
        }

        public JSchema ObterSchemaProdutoViewModel()
        {
            return JSchema.Parse(JsonConvert.SerializeObject(GerarProdutoViewModel()));
        }

        public ProdutoViewModel GerarProdutoViewModel()
        {
            return new Faker<ProdutoViewModel>("pt_BR")
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.CategoriaId, f => Guid.NewGuid())
                .RuleFor(p => p.Codigo, f => Convert.ToInt64(f.Commerce.Ean13()))
                .RuleFor(p => p.Descricao, f => f.Commerce.ProductName())
                .RuleFor(p => p.InformacoesAdicionais, f => f.Commerce.ProductMaterial())
                .RuleFor(p => p.DataHoraCriacao, f => f.Date.Recent(1))
                .RuleFor(p => p.DataHoraAlteracao, f => f.Date.Recent(1).OrNull(f))
                .Generate();
        }

        public void Dispose()
        { }
    }
}
