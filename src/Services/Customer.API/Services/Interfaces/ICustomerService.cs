namespace Customer.API.Services.Interfaces;

public interface ICustomerService
{
    Task<IResult> GetCustomerByName(string userName);
}