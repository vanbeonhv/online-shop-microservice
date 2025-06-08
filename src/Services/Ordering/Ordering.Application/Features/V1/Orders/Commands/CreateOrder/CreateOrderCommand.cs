using AutoMapper;
using EventBus.Message.IntegrationEvents.Events;
using Infrastructure.Mappings;
using MediatR;
using Ordering.Application.Common.Mapping;
using Ordering.Application.Features.V1.Orders.Common;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

public class CreateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<long>>, IMapFrom<BasketCheckoutEvent>
{
    public string UserName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
        profile.CreateMap<CreateOrderCommand, Order>();
    }
}