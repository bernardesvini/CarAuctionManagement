using CarAuctionManagement.DTOs.Enums;
using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.Handlers.Vehicles.GetVehicles;

public interface IGetVehiclesHandler
{
    GetVehiclesResponseDto GetVehiclesWithFilters(int? startYearFilter, int? endYearFilter, Guid? idFilter, VehicleType? typeFilter, string? manufacturerFilter, string? modelFilter, int page, int pageSize);
}