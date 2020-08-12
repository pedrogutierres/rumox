using AutoMapper;
using Catalogo.Domain.Produtos;
using Catalogo.Domain.Produtos.Interface;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rumox.API.ResponseType;
using Rumox.API.ViewModels.Produtos;
using Rumox.API.ViewModelsGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rumox.API.Controllers
{
    [Route("catalogo/produtos")]
    [Authorize]
    public class ProdutosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(
            IMapper mapper,
            IProdutoService produtoService,
            IProdutoRepository produtoRepository,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator)
            : base(notifications, user, mediator)
        {
            _mapper = mapper;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProdutoViewModel>> ObterProdutos([FromQuery]SituacaoQueryModel situacao = SituacaoQueryModel.Ativo)
        {
            var produtos = _produtoRepository.Buscar(p => true);
            produtos = situacao switch
            {
                SituacaoQueryModel.Ativo => produtos.Where(p => p.Ativo),
                SituacaoQueryModel.Inativo => produtos.Where(p => !p.Ativo),
                _ => produtos
            };

            return _mapper.Map<List<ProdutoViewModel>>(produtos);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoViewModel>> ObterProdutoPorId([FromRoute]Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
                return NotFound();

            return _mapper.Map<ProdutoViewModel>(produto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarProduto([FromBody]RegistrarProdutoViewModel model)
        {
            var produto = new Produto(model.Id, model.CategoriaId, model.Codigo, model.Descricao, model.InformacoesAdicionais);

            await _produtoService.Registrar(produto);

            return Response(produto.Id);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarProduto([FromRoute]Guid id, [FromBody]AtualizarProdutoViewModel model)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
            {
                NotificarErro("AtualizarProduto", "Produto não encontrado.");
                return BadRequest();
            }

            if (produto.CategoriaId != model.CategoriaId)
                produto.AlterarCategoria(model.CategoriaId);

            produto.AlterarDescricaoEInformacoes(model.Descricao, model.InformacoesAdicionais);

            await _produtoService.Atualizar(produto);

            return Response(produto.Id);
        }

        [HttpPatch("{id:guid}/situacao")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarSituacaoProduto([FromRoute]Guid id, [FromBody]AlterarSituacaoProdutoViewModel model)
        {
            if (model.Ativo)
                await _produtoService.Ativar(id);
            else
                await _produtoService.Inativar(id);

            return Response(id);
        }
    }
}
