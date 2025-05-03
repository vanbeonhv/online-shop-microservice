using AutoMapper;
using Infrastructure.Mappings;
using Product.API.Entities;
using Shared.DTOs.Product;

namespace Product.API;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CatalogProduct, ProductDto>();
        CreateMap<ProductCreateDto, CatalogProduct>();
        CreateMap<ProductUpdateDto, CatalogProduct>().IgnoreAllNonExisting();
    }
}