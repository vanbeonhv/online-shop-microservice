using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IResult> GetCustomerByName(string userName)
    {
        var customer = await _customerRepository.GetCustomerByName(userName);
        return customer == null
            ? Results.NotFound($"Customer with username {userName} not found.")
            : Results.Ok(customer);
    }
}