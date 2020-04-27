using CRM.Events.Clientes;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Domain.Clientes.Events
{
    public class ClienteEventHandler :
        INotificationHandler<ClienteRegistradoEvent>,
        INotificationHandler<ClienteAtualizadoEvent>,
        INotificationHandler<ClienteEmailAlteradoEvent>,
        INotificationHandler<ClienteContaCanceladaEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ClienteAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ClienteEmailAlteradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ClienteContaCanceladaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
