using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.Handlers.Vehicles.GetVehicles;

public interface IGetVehiclesHandler
{
    GetVehiclesResponseDto GetVehicles();
    GetVehiclesResponseDto GetVehiclesWithFilters(int? yearFilter, string? typeFilter, string? manufacturerFilter, string? modelFilter);
}