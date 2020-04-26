using Core.Domain.Models;
using Core.Domain.ValueObjects;
using CRM.Domain.Clientes.Validations;
using CRM.Domain.Clientes.ValuesObjects;
using System;

namespace CRM.Domain.Clientes
{
    public class Cliente : Entity<Cliente>
    {
        public CPF CPF { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public ClienteSenha Senha { get; private set; }
        public bool Ativo { get; private set; } = true;
        public DateTime DataHoraCriacao { get; private set; }
        public DateTime? DataHoraAlteracao { get; private set; }

        private Cliente() { }
        public Cliente(Guid id, CPF cpf, string nome, string sobrenome, string email, DateTime dataHoraCadastro, ClienteSenha senha)
        {
            Id = id;
            CPF = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            DataHoraCriacao = dataHoraCadastro;
        }

        public void AlterarDados(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public void AlterarEmail(string email)
        {
            Email = email;
        }

        public void CancelarConta()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ClienteEstaConsistenteValidation(this).Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
