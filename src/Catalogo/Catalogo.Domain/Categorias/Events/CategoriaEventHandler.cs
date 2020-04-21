using Catalogo.Events.Categorias;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalogo.Domain.Categorias.Events
{
    public class CategoriaEventHandler :
        INotificationHandler<CategoriaRegistradaEvent>,
        INotificationHandler<CategoriaAtualizadaEvent>,
        INotificationHandler<CategoriaDeletadaEvent>
    {
        public Task Handle(CategoriaRegistradaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(CategoriaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(CategoriaDeletadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
