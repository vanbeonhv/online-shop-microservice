using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Order;

namespace Saga.Orchestrator.HttpRepository;

public class OrderHttpRepository : IOrderHttpRepository
{
    private readonly HttpClient _httpClient;

    public OrderHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<long> CreateOrder(CreateOrderDto order)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDto> GetOrder(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteOrder(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteOrderByDocumentNo(string documentNo)
    {
        throw new NotImplementedException();
    }
}