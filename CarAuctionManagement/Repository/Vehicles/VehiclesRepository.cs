using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Repository.Vehicles;

public class VehiclesRepository : IVehiclesRepository
{
    private readonly List<Vehicle?> _vehicles = new();

    public void AddVehicle(Vehicle? vehicle)
    {
        _vehicles.Add(vehicle);
    }

    public List<Vehicle?> GetVehicles()
    {
        return _vehicles;
    }

    public void UpdateVehicle(Vehicle? vehicle)
    {
        _vehicles.Where(existingVehicle => existingVehicle?.Id == vehicle?.Id)
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
        _vehicles.Remove(vehicle);
    }
}