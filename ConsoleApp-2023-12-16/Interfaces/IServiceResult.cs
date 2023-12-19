using ConsoleApp_2023_12_16.Enums;

namespace ConsoleApp_2023_12_16.Interfaces;

public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}