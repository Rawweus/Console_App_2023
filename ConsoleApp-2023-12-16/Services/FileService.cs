using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp_2023_12_16.Models;
using System.Text.Json;
using ConsoleApp_2023_12_16.Interfaces;

namespace ConsoleApp_2023_12_16.Services
{

    public class FileService (string fileName) // Skriver till Json filen eller läser från Json filen
    {
        private readonly string _filePath = Path.Combine(Environment.CurrentDirectory, fileName); //Pekar mot directoryn som projektet ligger i. OBS: går in i bin/Debug/net8.0 om kör i debug.
        //Json filen heter CustomerData.json
        public bool SaveContentFromFile(List<ICustomer> customers) //Denna skriver till Json filen
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true }; //försäkrar att Json.Filen är läsbar (fancy sätt)
                var json = JsonSerializer.Serialize(customers, options); //serializer = gör objektet till ett json objekt
                File.WriteAllText(_filePath, json); //skriver customerdata till json objeketet
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;

        }

        public List<Customer>? GetContentFromFile() //Denna läser från Json filen
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath); //Läser allt inom json objektet
                    var customers = JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>(); //json objektet sätts in i en variabel som är List<Customer>
                    return customers;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!;
        }
    }
}
