using Xunit;

namespace Catalogo.Domain.Tests.Categorias
{
    [Collection(nameof(CategoriaCollection))]
    public class CategoriaTests
    {
        private readonly CategoriaTestsFixture _categoriaTestsFixture;

        public CategoriaTests(CategoriaTestsFixture categoriaTestsFixture)
        {
            _categoriaTestsFixture = categoriaTestsFixture;
        }

        [Fact(DisplayName = "Instanciar categoria válida.")]
        [Trait("Categoria", "Domínio")]
        public void Categoria_Instanciar_Valida()
        {
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();
            Assert.True(categoria.EhValido());
        }

        [Fact(DisplayName = "Instanciar categoria inválida.")]
        [Trait("Categoria", "Domínio")]
        public void Categoria_Instanciar_Invalida()
        {
            var categoria = _categoriaTestsFixture.GerarCategoriaInvalida();
            Assert.False(categoria.EhValido());
        }

        [Fact(DisplayName = "Alterar nome da categoria com sucesso.")]
        [Trait("Categoria", "Domínio")]
        public void Categoria_AlterarNome_ComSucesso()
        {
            var categoria = _categoriaTestsFixture.GerarCategoriaValida();

            var novoNome = "Lazer";
            categoria.AlteraNome(novoNome);
            
            Assert.Equal(novoNome, categoria.Nome);
        }
    }
}
