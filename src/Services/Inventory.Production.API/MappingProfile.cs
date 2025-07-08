using AutoMapper;
using Shared.DTOs.Inventory;

namespace Inventory.Production.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InventoryDto, Entities.Inventory>().ReverseMap();
        CreateMap<PurchaseProductDto, InventoryDto>();
        
    }
}