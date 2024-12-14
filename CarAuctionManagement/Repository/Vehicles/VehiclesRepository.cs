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
        return _database.Vehicles.Where(vehicle => vehicle != null && !vehicle.GetIsDeleted()).ToList();
    }

    public Vehicle? UpdateVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Where(existingVehicle => existingVehicle?.GetId() == vehicle?.GetId())
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

    public void RemoveVehicle(Guid? vehicleId)
    {
        _database.Vehicles.Where(vehicle => vehicle?.GetId() == vehicleId).ToList()?.ForEach(vehicle =>
        {
            vehicle?.SetIsDeleted(true);
        });
    }
}