using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Database;

namespace CarAuctionManagement.Repository.Vehicles;

public class VehiclesRepository : IVehiclesRepository
{
    private readonly InMemoryDatabase _database;
    
    public VehiclesRepository(InMemoryDatabase database)
    {
        _database = database;
    }

    public Vehicle? AddVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Add(vehicle);
        return vehicle;
    }

    public List<Vehicle?> GetVehicles()
    {
        return _database.Vehicles;
    }

    public Vehicle? UpdateVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Where(existingVehicle => existingVehicle?.Id == vehicle?.Id)
            .ToList()
            .ForEach(existingVehicle =>
                {
                    if (existingVehicle != null)
                    {
                       var index = _database.Vehicles.IndexOf(existingVehicle);
                          _database.Vehicles[index] = vehicle;
                    }
                }
            );
        return vehicle;
    }

    public void RemoveVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Remove(vehicle);
    }
}