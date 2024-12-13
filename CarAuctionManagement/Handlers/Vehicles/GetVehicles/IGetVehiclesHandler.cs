using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.Handlers.Vehicles.GetVehicles;

public interface IGetVehiclesHandler
{
    GetVehicleshResponseDto GetVehicles();
    GetVehicleshResponseDto GetVehiclesWithFilters(int? yearFilter, string? typeFilter, string? manufacturerFilter, string? modelFilter);
}