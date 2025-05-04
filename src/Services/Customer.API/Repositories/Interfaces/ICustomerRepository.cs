using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryQueryBase<Entities.Customer, long, CustomerContext>
{
    Task<Entities.Customer> GetCustomerByName(string userName);
}