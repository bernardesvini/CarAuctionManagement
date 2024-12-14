using CarAuctionManagement.DTOs.Enums;
using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.Handlers.Vehicles.GetVehicles;

public interface IGetVehiclesHandler
{
    GetVehiclesResponseDto GetVehicles();
    GetVehiclesResponseDto GetVehiclesWithFilters(int? yearFilter, VehicleType? typeFilter, string? manufacturerFilter, string? modelFilter);
}