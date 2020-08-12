using Catalogo.Domain.Produtos.Validations;
using Core.Domain.Models;
using System;

namespace Catalogo.Domain.Produtos
{
    public sealed class Produto : Entity<Produto>
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

            if (codigo == 0)
                Codigo = GerarCodigo();
        }

        public void AlterarCategoria(Guid categoriaId)
        {
            CategoriaId = categoriaId;
            NotificarAlteracao();
        }

        public void AlterarDescricaoEInformacoes(string descricao, string informacoesAdicionais)
        {
            Descricao = descricao;
            InformacoesAdicionais = informacoesAdicionais;
            NotificarAlteracao();
        }

        private int GerarCodigo()
        {
            return new Random().Next(1, 999999999);
        }

        public void Ativar()
        {
            Ativo = true;
            NotificarAlteracao();
        }
        public void Inativar()
        {
            Ativo = false;
            NotificarAlteracao();
        }

        private void NotificarAlteracao()
        {
            DataHoraAlteracao = DateTime.UtcNow;
        }

        public override bool EhValido()
        {
            ValidationResult = new ProdutoEstaConsistenteValidation(this).Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
