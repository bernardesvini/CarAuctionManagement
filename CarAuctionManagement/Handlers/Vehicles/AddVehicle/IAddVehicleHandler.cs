using CarAuctionManagement.DTOs.Vehicles;
using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Handlers.Vehicles.AddVehicle;

public interface IAddVehicleHandler
{
    Vehicle AddVehicle(VehicleDto vehicleDto);
}