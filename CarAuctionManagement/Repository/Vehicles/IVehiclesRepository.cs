using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Repository.Vehicles;

public interface IVehiclesRepository
{
    Vehicle? AddVehicle(Vehicle? vehicle);
    List<Vehicle?> GetVehicles();
    Vehicle? UpdateVehicle(Vehicle? vehicle);
    void RemoveVehicle(Guid? vehicleId);
}