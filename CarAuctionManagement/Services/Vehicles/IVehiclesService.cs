using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Services.Vehicles;

public interface IVehiclesService
{
    Vehicle? AddVehicle(Vehicle? vehicle);
    List<Vehicle?>? GetVehicles();
    List<Vehicle?>? GetVehicleSearch(string? type = null, string? manufacturer = null, string? model = null, int? startYear = null, int? endYear = null, Guid? id = null);
    Vehicle? UpdateVehicle(Vehicle? vehicle);
    void RemoveVehicle(Guid? id);
    Vehicle? GetVehicleById(Guid? id);
}