using Xunit;
using ConsoleApp_2023_12_16.Models;
using ConsoleApp_2023_12_16.Services;
using ConsoleApp_2023_12_16.Interfaces;
using System;

namespace ConsoleApp_2023_12_16.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void AddCustomerToList_ShouldAddCustomer_WhenNewEmail() // H�r k�r jag mitt f�rsta test d�r jag l�gger till en kund/kontakt
        {
            // Arrange
            var service = new CustomerService();
            var customer = new Customer { Email = "name@example.com", FirstName = "John", LastName = "Doe" };

            // Act
            service.RemoveCustomerByEmail(customer.Email); //Detta �r f�r att rensa test milj�ns CustomerData.json vilket sparas i test-mappen under debug pga pathen
            var result = service.AddCustomerToList(customer);

            // Assert
            Assert.Equal(Enums.ServiceStatus.SUCCESSED, result.Status);
        }

        [Fact]
        public void RemoveCustomerByEmail_ShouldRemoveCustomer_WhenEmailExists() // // H�r k�r jag mitt andra test d�r jag tar bort en kund/kontakt. Eftersom b�da �r inuti CustomerService s� lade jag dem i samma klass.
        {
            // Arrange
            var service = new CustomerService();
            var customer = new Customer { Email = "test@example.com", FirstName = "Test", LastName = "User" };

            // Act
            service.AddCustomerToList(customer);
            var removeResult = service.RemoveCustomerByEmail("test@example.com");
            var getResult = service.GetCustomersFromList();

            // Assert
            Assert.Equal(Enums.ServiceStatus.SUCCESSED, removeResult.Status);
            Assert.DoesNotContain(customer, (List<Customer>)getResult.Result);
        }
    }
}
