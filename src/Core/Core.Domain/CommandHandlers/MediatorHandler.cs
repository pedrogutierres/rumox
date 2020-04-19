using Core.Domain.Commands;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.CommandHandlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public Task SendCommand<T>(T comando, CancellationToken cancellation = default(CancellationToken)) where T : Command
        {
            return _mediator.Send(comando, cancellation);
        }

        public Task RaiseEvent<T>(T evento, CancellationToken cancellation = default(CancellationToken)) where T : Event
        {
            if (!evento.MessageType.Equals(nameof(DomainNotification)))
                _eventStore?.SalvarEvento(evento);

            return _mediator.Publish(evento, cancellation);
        }
    }
}
