using Core.Domain.Events;
using Core.Domain.ValueObjects;
using System;

namespace CRM.Events.Clientes
{
    public class ClienteContaCanceladaEvent : Event
    {
        public Guid Id { get; private set; }
        public CPF CPF { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }

        public ClienteContaCanceladaEvent(Guid id, CPF cpf, string nome, string sobrenome, string email, string senha, bool ativo)
        {
            AggregateId = id;
            Id = id;
            CPF = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            Ativo = ativo;
        }
    }
}
