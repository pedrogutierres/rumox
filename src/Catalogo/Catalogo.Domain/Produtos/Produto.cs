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
        public DateTime DataHoraCriacao { get; private set; } = DateTime.Now;
        public DateTime? DataHoraAlteracao { get; private set; }

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

        public bool GerarCodigo()
        {
            if (Codigo > 0) return false;

            Codigo = new Random().Next(1, 999999999);
            return true;
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
    }
}
