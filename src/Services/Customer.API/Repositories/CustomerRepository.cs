using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories;

public class CustomerRepository :
    RepositoryQueryBaseAsync<Entities.Customer, long, CustomerContext>, ICustomerRepository
{
    public CustomerRepository(CustomerContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Entities.Customer> GetCustomerByName(string userName)
    {
        return await FindByCondition(x => x.UserName == userName).FirstOrDefaultAsync();
    }
}