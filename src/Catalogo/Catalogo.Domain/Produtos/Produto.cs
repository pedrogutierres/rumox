using Catalogo.Domain.Produtos.Validations;
using Core.Domain.Models;
using System;

namespace Catalogo.Domain.Produtos
{
    public class Produto : Entity<Produto>
    {
        public Guid CategoriaId { get; private set; }
        public long Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesAdicionais { get; private set; }
        public bool Ativo { get; private set; } = true;

        private Produto() { }
        public Produto(Guid id, Guid categoriaId, long codigo, string descricao, string informacoesAdicionais)
        {
            Id = id;
            CategoriaId = categoriaId;
            Codigo = codigo;
            Descricao = descricao;
            InformacoesAdicionais = informacoesAdicionais;
        }

        public void AlterarCategoria(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public void AlterarDados(string descricao, string informacoesAdicionais)
        {
            Descricao = descricao;
            InformacoesAdicionais = informacoesAdicionais;
        }

        public void Ativar()
        {
            Ativo = true;
        }
        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ProdutoEstaConsistenteValidation(this).Validate(this);

            return ValidationResult.IsValid;
        }

        public static long GerarCodigoAleatorio()
        {
            return new Random().Next(1, 999999999);
        }
    }
}
