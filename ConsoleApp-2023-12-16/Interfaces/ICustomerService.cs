using ConsoleApp_2023_12_16.Models;
using ConsoleApp_2023_12_16.Models.Responses;

namespace ConsoleApp_2023_12_16.Interfaces;

public interface ICustomerService
{
    IServiceResult AddCustomerToList(ICustomer customer);
    IServiceResult GetCustomersFromList();
    IServiceResult RemoveCustomerByEmail(string email);
}
