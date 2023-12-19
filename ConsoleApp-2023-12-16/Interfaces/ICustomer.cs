namespace ConsoleApp_2023_12_16.Interfaces
{
    public interface ICustomer
    {
        string Email { get; set; }
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        string? PhoneNumber { get; set; }
        string Address { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }

    }
}