using ConsoleApp_2023_12_16.Interfaces;
using ConsoleApp_2023_12_16.Models;

namespace ConsoleApp_2023_12_16.Services;

public interface IMenuService
{
    void ShowMainMenu();

}
public class MenuService : IMenuService
{

    private readonly ICustomerService _customerService = new CustomerService();

    public void ShowMainMenu() // Självaste menyn
    {
        while (true)
        {

            DisplayMenuTitle("MENU OPTIONS");
            Console.WriteLine($"{"1.",-4} Add New Customer");
            Console.WriteLine($"{"2.",-4} View Customer List");
            Console.WriteLine($"{"3.",-4} Remove Customer from List");
            Console.WriteLine($"{"0.",-4} Exit Application");
            Console.WriteLine();
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAddCustomerOption();
                    break;

                case "2":
                    ShowViewCustomerListOption();
                    break;

                case "3":
                    ShowRemoveCustomerOption();
                    break;

                case "0":
                    ShowExitApplicationOption();
                    break;

                default:
                    Console.WriteLine("\nInvalid Option selected. Press any key to try again");
                    Console.ReadKey();
                    break;
            }


        }
    }

    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to close this application? (y/n)");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            Environment.Exit(0);
        }
    }



    private void ShowAddCustomerOption()
    {
        ICustomer customer = new Customer();

        DisplayMenuTitle("Add New Customer");

        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine()!;

        Console.Write("E-mail: ");
        customer.Email = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        customer.PhoneNumber = Console.ReadLine()!;

        Console.Write("Address: ");
        customer.Address = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        customer.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        customer.City = Console.ReadLine()!;

        var res = _customerService.AddCustomerToList(customer);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("The customer was added successfully.");
                break;

            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("The customer already exists");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed when trying to add the customer to the customer list.");
                Console.WriteLine("See error message :: " + res.Result.ToString());
                break;
        }


        DisplayPressAnyKey();
    }

    private void ShowViewCustomerListOption()
    {
        DisplayMenuTitle("Customer List");
        var res = _customerService.GetCustomersFromList(); //Res innehåller listan om det funkade, och status på om läsningen funkade som det skulle eller inte
        List<Customer> customerList = (List<Customer>)res.Result; //Listan läggs till något mer läsbart (detta är bara för att komma ihåg när man läser vidare)

        if (res.Status == Enums.ServiceStatus.SUCCESSED) //Kollar om det funkade att läsa objektet. Om det funkade, så fortsätter den
        {
            if (customerList is List<Customer>) //Checkar att listan är korrekt enligt modellen
            {
                if (!customerList.Any()) //Checkar att det finns något i listan
                {
                    Console.WriteLine("No customers found.");
                }
                else
                {
                    foreach (var customer in customerList) //För varje Customer i CustomerList, skriv informationen i konsollen
                    {
                        Console.WriteLine($"{customer.FirstName}\n{customer.LastName}\n<{customer.Email}>\n{customer.Address}\n{customer.PostalCode}\n{customer.City}\n");
                    }
                }
            }
        }

        DisplayPressAnyKey();
    }




    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"##### {title} #####");
        Console.WriteLine("________________________");
    }


    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void ShowRemoveCustomerOption() // Ett nytt menyval och en metod för att hantera borttagning av kontakter
    {
        DisplayMenuTitle("Remove Customer");

        Console.Write("Enter the email of the customer to remove: ");
        var email = Console.ReadLine();

        var res = _customerService.RemoveCustomerByEmail(email);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("Customer successfully removed.");
                break;

            case Enums.ServiceStatus.NOT_FOUND:
                Console.WriteLine("Customer not found.");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("An error occurred: " + res.Result);
                break;
        }

        DisplayPressAnyKey();
    }


}