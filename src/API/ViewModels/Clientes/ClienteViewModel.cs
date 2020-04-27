using System;

namespace Rumox.API.ViewModels.Clientes
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
