using AutoMapper;
using Catalogo.Domain.Categorias;
using Catalogo.Domain.Produtos;
using CRM.Domain.Clientes;
using Rumox.API.ViewModels.Categorias;
using Rumox.API.ViewModels.Clientes;
using Rumox.API.ViewModels.Produtos;

namespace Rumox.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}

