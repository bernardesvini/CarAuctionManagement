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

    public Vehicle AddVehicle(Vehicle vehicle)
    {
        _database.Vehicles.Add(vehicle);
        return vehicle;
    }

    public List<Vehicle?> GetVehicles()
    {
        return _database.Vehicles;
    }

    public void UpdateVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Where(existingVehicle => existingVehicle?.Id == vehicle?.Id)
            .ToList()
            .ForEach(existingVehicle =>
                {
                    if (existingVehicle != null)
                    {
                        existingVehicle.Id = vehicle?.Id;
                        existingVehicle.Manufacturer = vehicle?.Manufacturer;
                        existingVehicle.Model = vehicle?.Model;
                        existingVehicle.Year = vehicle?.Year;
                        existingVehicle.StartingBid = vehicle?.StartingBid;
                    }
                    switch (existingVehicle)
                    {
                        case Sedan sedan when vehicle is Sedan updatedSedan:
                            sedan.NumberOfDoors = updatedSedan.NumberOfDoors;
                            break;
                        case Hatchback hatchback when vehicle is Hatchback updatedHatchback:
                            hatchback.NumberOfDoors = updatedHatchback.NumberOfDoors;
                            break;
                        case Suv suv when vehicle is Suv updatedSuv:
                            suv.NumberOfSeats = updatedSuv.NumberOfSeats;
                            break;
                        case Truck suv when vehicle is Truck updatedTruck:
                            suv.LoadCapacity = updatedTruck.LoadCapacity;
                            break;
                    }
                }
            );
    }

    public void RemoveVehicle(Vehicle? vehicle)
    {
        _database.Vehicles.Remove(vehicle);
    }
}