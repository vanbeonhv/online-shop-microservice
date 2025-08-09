using AutoMapper;
using EventBus.Message.IntegrationEvents.Events;
using Shared.DTOs.Basket;

namespace Basket.API;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<BasketCheckoutDto, BasketCheckoutEvent>();
    }
}