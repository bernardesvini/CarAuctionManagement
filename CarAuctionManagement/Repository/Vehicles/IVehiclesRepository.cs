using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Repository.Vehicles;

public interface IVehiclesRepository
{
    Vehicle AddVehicle(Vehicle vehicle);
    List<Vehicle?> GetVehicles();
    void UpdateVehicle(Vehicle? vehicle);
    void RemoveVehicle(Vehicle? vehicle);
}