using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.ResponseType;
using Rumox.API.ViewModelsGlobal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumox.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected readonly IUser _user;

        protected Guid UsuarioId { get; set; }

        protected BaseController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
            _user = user;

            if (user.Autenticado())
                UsuarioId = user.UsuarioId();
        }

        protected new IActionResult Response(object result = null) => Response<object>(result);
        protected new IActionResult Response(Guid id) => Response<object>(id: id);
        protected new IActionResult Response<T>(T result = default, Guid? id = null)
        {
            if (OperacaoValida())
            {
                return Ok(new ResponseSuccess<T>
                {
                    Data = result,
                    Id = id
                });
            }

            return new BadRequestObjectResult(
                new ResponseError(
                    "Erros de negócio",
                    "Erros nas regras de negócio encontradas",
                    StatusCodes.Status400BadRequest,
                    HttpContext.Request.Path,
                   _notifications.GetNotifications()
                ))
            {
                ContentTypes = { "application/problem+json" }
            };
        }

        protected new ActionResult BadRequest()
        {
            return new BadRequestObjectResult(
                 new ResponseError(
                     "Erros de negócio",
                     "Erros nas regras de negócio encontradas",
                     StatusCodes.Status400BadRequest,
                     HttpContext.Request.Path,
                     _notifications.GetNotifications()
                 ))
            {
                ContentTypes = { "application/problem+json" }
            };
        }

        protected PaginacaoViewModel<T> Paginacao<T>(IEnumerable<T> itens, int totalItens)
        {
            return PaginacaoViewModel<T>.NovaPaginacao(itens, totalItens);
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                NotificarErro(error.PropertyName, error.ErrorMessage);
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.RaiseEvent(new DomainNotification(codigo, mensagem));
        }
    }
}