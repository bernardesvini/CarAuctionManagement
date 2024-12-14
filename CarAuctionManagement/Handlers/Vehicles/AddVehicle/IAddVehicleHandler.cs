using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.Handlers.Vehicles.AddVehicle;

public interface IAddVehicleHandler
{
    VehicleResponseDto? AddVehicle(VehicleRequestDto vehicleRequestDto);
    VehicleResponseDto? UpdateVehicle(Guid id ,VehicleUpdateRequestDto vehicleUpdateRequestDto);
}