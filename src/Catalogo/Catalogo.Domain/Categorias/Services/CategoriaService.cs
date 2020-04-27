using Catalogo.Domain.Categorias.Events;
using Catalogo.Domain.Categorias.Interfaces;
using Catalogo.Domain.Categorias.Validations;
using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.Services;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Catalogo.Domain.Categorias.Services
{
    public class CategoriaService : DomainService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaService(
            ICategoriaRepository categoriaRepository,
            IProdutoRepository produtoRepository,
            IUnitOfWorkCatalogo uow,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, mediator, notifications)
        {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task Registrar(Categoria categoria)
        {
            if (!CategoriaValida(categoria))
                return;

            var validation = new CategoriaAptaParaRegistrarValidation(categoria, _categoriaRepository).Validate(categoria);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _categoriaRepository.Registrar(categoria);

            if (!Commit())
                return;

            await _mediator.RaiseEvent(CategoriaAdapter.ToCategoriaRegistradaEvent(categoria));
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!CategoriaValida(categoria))
                return;

            var validation = new CategoriaAptaParaRegistrarValidation(categoria, _categoriaRepository).Validate(categoria);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _categoriaRepository.Atualizar(categoria);

            if (!Commit())
                return;

            await _mediator.RaiseEvent(CategoriaAdapter.ToCategoriaAtualizadaEvent(categoria));
        }

        public async Task Deletar(Guid id)
        {
            var categoria = await ObterCategoria(id, "Deletar");
            if (categoria == null) return;

            var validation = new CategoriaAptaParaDeletarValidation(categoria, _produtoRepository).Validate(categoria);
            if (!validation.IsValid)
            {
                NotificarValidacoesErro(validation);
                return;
            }

            await _categoriaRepository.Deletar(id);

            if (!Commit())
                return;

            await _mediator.RaiseEvent(CategoriaAdapter.ToCategoriaDeletadaEvent(categoria));
        }

        private async Task<bool> CategoriaExistente(Guid id)
        {
            return await _categoriaRepository.ExistePorId(id);
        }

        private async Task<Categoria> ObterCategoria(Guid id, string messageType)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria != null) return categoria;

            await _mediator.RaiseEvent(new DomainNotification(messageType, "Categoria não encontrada."));
            return null;
        }

        private bool CategoriaValida(Categoria categoria)
        {
            if (categoria) return true;

            NotificarValidacoesErro(categoria.ValidationResult);
            return false;
        }
    }
}
