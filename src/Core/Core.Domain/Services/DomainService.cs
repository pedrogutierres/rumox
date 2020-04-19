using Core.Domain.Extensions;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using FluentValidation.Results;
using MediatR;
using System;
using System.Text;

namespace Core.Domain.Services
{
    public abstract class DomainService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler _notifications;

        protected DomainService(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected void NotificarErro(string nome, string mensagem)
        {
            _mediator.RaiseEvent(new DomainNotification(nome, mensagem));
        }

        protected bool HasNotifications()
        {
            return _notifications.HasNotifications();
        }

        protected bool Commit()
        {
            if (HasNotifications()) return false;
            try
            {
                if (_uow.Commit())
                    return true;
            }
            catch (Exception ex)
            {
                NotificarErro("Commit", "Erro ao tentar salvar os dados no banco de dados, erro: " + ex.AgruparTodasAsMensagens());
                return false;
            }

            NotificarErro("Commit", "Não foi possível salvar os dados no banco de dados.");
            return false;
        }
    }
}