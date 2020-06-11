using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Produtos.Events;
using Catalogo.Domain.Produtos.Interface;
using Catalogo.Domain.Produtos.Validations;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.Services;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Catalogo.Domain.Produtos.Services
{
    public class ProdutoService : DomainService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(
            IProdutoRepository produtoRepository,
            IUnitOfWorkCatalogo uow, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) 
            : base(uow, mediator, notifications)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Registrar(Produto produto)
        {
            if (!ProdutoValido(produto))
                return;

            var validation = new ProdutoAptoParaRegistrarValidation(produto, _produtoRepository).Validate(produto);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _produtoRepository.Registrar(produto);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ProdutoAdapter.ToProdutoRegistradoEvent(produto));
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ProdutoValido(produto))
                return;

            var validation = new ProdutoAptoParaAtualizarValidation(produto, _produtoRepository).Validate(produto);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _produtoRepository.Atualizar(produto);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ProdutoAdapter.ToProdutoAtualizadoEvent(produto));
        }

        public async Task Ativar(Guid id)
        {
            var produto = await ObterProduto(id, "Ativar");
            if (produto == null) return;

            produto.Ativar();

            await _produtoRepository.Atualizar(produto);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ProdutoAdapter.ToProdutoAtivadoEvent(id));
        }

        public async Task Inativar(Guid id)
        {
            var produto = await ObterProduto(id, "Inativar");
            if (produto == null) return;

            produto.Inativar();

            await _produtoRepository.Atualizar(produto);

            if (!await Commit())
                return;

            await _mediator.RaiseEvent(ProdutoAdapter.ToProdutoInativadoEvent(id));
        }

        private async Task<bool> ProdutoExistente(Guid id)
        {
            return await _produtoRepository.ExistePorId(id);
        }

        private async Task<Produto> ObterProduto(Guid id, string messageType)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto != null) return produto;

            await _mediator.RaiseEvent(new DomainNotification(messageType, "Produto não encontrado."));
            return null;
        }

        private bool ProdutoValido(Produto produto)
        {
            if (produto) return true;

            NotificarValidacoesErro(produto.ValidationResult);
            return false;
        }
    }
}
