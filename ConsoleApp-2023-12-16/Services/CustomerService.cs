using ConsoleApp_2023_12_16.Interfaces;
using ConsoleApp_2023_12_16.Models;
using ConsoleApp_2023_12_16.Models.Responses;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp_2023_12_16.Services;

public class CustomerService : ICustomerService
{
    private static List<ICustomer> _customers = new List<ICustomer>();
    private readonly FileService _fileService = new FileService("CustomerData.json"); //Deklarerar vilken fil man skriver/läser till

    public CustomerService()
    {
        LoadCustomersFromFile();
    }

    private void LoadCustomersFromFile() //Ser till att befintliga kontakter lassar in i _customers utan att den skriver över gamla kontakter.
    {
        var existingCustomers = _fileService.GetContentFromFile();
        if (existingCustomers != null)
        {
            _customers = existingCustomers.Cast<ICustomer>().ToList();
        }
    }

    public IServiceResult AddCustomerToList(ICustomer customer) //Detta är 1:an från Konsollen. Skriver in customers till listan i Json Objektet.
    {
        var response = new ServiceResult();

        try
        {
            if (!_customers.Any(x => x.Email == customer.Email)) //Kollar om Email är samma som i listan (om det redan finns email, så säger den 'already exists')
            {
                _customers.Add(customer); //Lägger customer till listan _customers
                _fileService.SaveContentFromFile(_customers); //den nya _customers skriver över den gamla _customers i filen
                response.Status = Enums.ServiceStatus.SUCCESSED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    public IServiceResult GetCustomersFromList() //Det här är 2:an från Konsollen. Läser listan av Customers från Json objektet.
    {
        var response = new ServiceResult();

        try
        {
            response.Status = Enums.ServiceStatus.SUCCESSED;
            List<Customer>? jsonContent = _fileService.GetContentFromFile(); //Här körs funktionen för att läsa listan och lägga listan i ett objekt
            response.Result = jsonContent!; //här skickas listan av customers till konsollen (MenuService)
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    public IServiceResult RemoveCustomerByEmail(string email) //För att ta bort en kontakt
    {
        var response = new ServiceResult();

        try
        {
            var customer = _customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                _customers.Remove(customer);
                _fileService.SaveContentFromFile(_customers); // Uppdatera filen efter borttagningen
                response.Status = Enums.ServiceStatus.SUCCESSED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }
}
