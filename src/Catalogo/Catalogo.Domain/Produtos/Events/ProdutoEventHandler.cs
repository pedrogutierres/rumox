using Catalogo.Events.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Events
{
    public class ProdutoEventHandler :
        INotificationHandler<ProdutoRegistradoEvent>,
        INotificationHandler<ProdutoAtualizadoEvent>,
        INotificationHandler<ProdutoAtivadoEvent>,
        INotificationHandler<ProdutoInativadoEvent>
    {
        public Task Handle(ProdutoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProdutoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProdutoAtivadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProdutoInativadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
