using AutoMapper;
using Everton._123Vendas.API.Models.Request;
using Everton._123Vendas.API.Models.Response;
using Everton._123Vendas.Domain.Entities;

namespace Everton._123Vendas.API.Mappers;

public class CompraProfile : Profile
{
    public CompraProfile()
    {
        CreateMap<ItemCompraRequest, ItemCompra>();
        CreateMap<CompraRequest, Compra>();

        CreateMap<ItemCompra, ItemCompraResponse>();
        CreateMap<Compra, CompraResponse>();
    }
}
