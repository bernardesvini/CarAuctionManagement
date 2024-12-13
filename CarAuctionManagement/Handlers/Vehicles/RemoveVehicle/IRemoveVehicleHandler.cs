using CarAuctionManagement.DTOs.Vehicles.Requests;

namespace CarAuctionManagement.Handlers.Vehicles.RemoveVehicle;

public interface IRemoveVehicleHandler
{
    void RemoveVehicle(VehicleRemoveRequestDto id);
}