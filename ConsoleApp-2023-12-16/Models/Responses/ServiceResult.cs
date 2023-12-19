using ConsoleApp_2023_12_16.Enums;
using ConsoleApp_2023_12_16.Interfaces;

namespace ConsoleApp_2023_12_16.Models.Responses;
public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}
