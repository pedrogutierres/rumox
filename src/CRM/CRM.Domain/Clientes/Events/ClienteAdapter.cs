using CRM.Events.Clientes;

namespace CRM.Domain.Clientes.Events
{
    internal static class ClienteAdapter
    {
        public static ClienteRegistradoEvent ToClienteRegistradoEvent(Cliente cliente)
        {
            return new ClienteRegistradoEvent(cliente.Id, cliente.CPF, cliente.Nome, cliente.Sobrenome, cliente.Email, cliente.Ativo);
        }

        public static ClienteAtualizadoEvent ToClienteAtualizadoEvent(Cliente cliente)
        {
            return new ClienteAtualizadoEvent(cliente.Id, cliente.Nome, cliente.Sobrenome);
        }

        public static ClienteEmailAlteradoEvent ToClienteEmailAlteradoEvent(Cliente cliente)
        {
            return new ClienteEmailAlteradoEvent(cliente.Id, cliente.Email);
        }

        public static ClienteContaCanceladaEvent ToClienteContaCanceladaEvent(Cliente cliente)
        {
            return new ClienteContaCanceladaEvent(cliente.Id, cliente.CPF, cliente.Nome, cliente.Sobrenome, cliente.Email, cliente.Ativo);
        }
    }
}
