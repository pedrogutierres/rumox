using AutoMapper;
using Catalogo.Domain.Categorias;
using Catalogo.Domain.Categorias.Interfaces;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.ResponseType;
using Rumox.API.ViewModels.Categorias;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rumox.API.Controllers
{
    [Route("catalogo/categorias")]
    public class CategoriasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _categoriaService;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(
            IMapper mapper,
            ICategoriaService categoriaService,
            ICategoriaRepository categoriaRepository,
            INotificationHandler<DomainNotification> notifications, 
            IUser user, 
            IMediatorHandler mediator) 
            : base(notifications, user, mediator)
        {
            _mapper = mapper;
            _categoriaService = categoriaService;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            return _mapper.Map<List<CategoriaViewModel>>(_categoriaRepository.Buscar(p => true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaViewModel>> ObterCategoriaPorId([FromRoute]Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);
            if (categoria == null)
                return NotFound();

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarCategoria([FromBody]RegistrarCategoriaViewModel model)
        {
            var categoria = new Categoria(model.Id, model.Nome);

            await _categoriaService.Registrar(categoria);

            return Response(categoria.Id);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarCategoria([FromRoute]Guid id, [FromBody]AtualizarCategoriaViewModel model)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);
            if (categoria == null)
            {
                NotificarErro("AtualizarCategoria", "Categoria não encontrada.");
                return BadRequest();
            }

            categoria.AlteraNome(model.Nome);

            await _categoriaService.Atualizar(categoria);

            return Response(categoria.Id);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarCategoria([FromRoute]Guid id)
        {
            await _categoriaService.Deletar(id);

            return Response(id);
        }
    }
}
