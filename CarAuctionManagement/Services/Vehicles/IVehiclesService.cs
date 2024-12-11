using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Services.Vehicles;

public interface IVehiclesService
{
    void AddVehicle(Vehicle? vehicle);
    List<Vehicle?> GetVehicles();
    List<Vehicle?>? GetVehicleSearch(string? type = null, string? manufacturer = null, string? model = null, int? year = null);
    void UpdateVehicle(Vehicle? vehicle);
    void RemoveVehicle(string? id);
    Vehicle? GetVehicleById(string? id);
}