using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Vehicles;

namespace CarAuctionManagement.Services.Vehicles;

public class VehiclesService : IVehiclesService
{
    private readonly IVehiclesRepository _vehiclesRepository;

    public VehiclesService(IVehiclesRepository vehiclesRepository)
    {
        _vehiclesRepository = vehiclesRepository;
    }

    public Vehicle? AddVehicle(Vehicle? vehicle)
    {
        vehicle?.Validate();
        List<Vehicle?> existingVehicles = _vehiclesRepository.GetVehicles();
        VehicleValidations(vehicle, existingVehicles);
        Vehicle? response = _vehiclesRepository.AddVehicle(vehicle);
        return response;
    }

    public Vehicle GetVehicleById(Guid? id)
    {
        Vehicle vehicle = GetVehicleByIdValidation(id);
        return vehicle;
    }

    public List<Vehicle?> GetVehicles()
    {
        List<Vehicle?> vehicles = _vehiclesRepository.GetVehicles();
        GetVehiclesValidation(vehicles);
        return vehicles;
    }

    public List<Vehicle?> GetVehicleSearch(string? type = null, string? manufacturer = null, string? model = null, int? startYear = null, int? endYear = null, Guid? id = null)
    {
        return ApplyFilterOptions(type, manufacturer, model, startYear, endYear, id);
    }

    public Vehicle? UpdateVehicle(Vehicle? vehicle)
    {
        List<Vehicle?> existingVehicles = _vehiclesRepository.GetVehicles();
        VehicleValidations(vehicle, existingVehicles, true);
        Vehicle? vehicleToUpdate = UpdateVehicleType(vehicle, existingVehicles);
        Vehicle? response = _vehiclesRepository.UpdateVehicle(vehicleToUpdate);
        return response;
    }

    public void RemoveVehicle(Guid? id)
    {
        Vehicle vehicleToRemoved = GetVehicleByIdValidation(id);
        _vehiclesRepository.RemoveVehicle(vehicleToRemoved.GetId());
    }

    private void GetVehiclesValidation(List<Vehicle?>? vehicles)
    {
        if (vehicles?.Count == 0)
            throw new CustomExceptions.NoVehiclesFoundException();
    }

    private Vehicle GetVehicleByIdValidation(Guid? id)
    {
        List<Vehicle?> allVehicles = _vehiclesRepository.GetVehicles();
        if (allVehicles.Count == 0)
            throw new CustomExceptions.NoVehiclesFoundException();

        Vehicle? vehicle = allVehicles.FirstOrDefault(vehicle => vehicle?.GetId() == id);

        if (vehicle == null)
            throw new CustomExceptions.VehicleNotFoundException(id);

        return vehicle;
    }

    private List<Vehicle?> ApplyFilterOptions(string? type = null, string? manufacturer = null, string? model = null, int? startYear = null, int? endYear = null, Guid? id = null)
    {
        List<Vehicle?> vehicles = _vehiclesRepository.GetVehicles();

        if (vehicles.Count == 0)
            throw new CustomExceptions.NoVehiclesFoundException();

        List<Vehicle?> filteredVehicles = vehicles
            .Where(vehicle =>
                (type == null ||
                 (type.Equals("Sedan", StringComparison.OrdinalIgnoreCase) && vehicle is Sedan) ||
                 (type.Equals("Hatchback", StringComparison.OrdinalIgnoreCase) && vehicle is Hatchback) ||
                 (type.Equals("SUV", StringComparison.OrdinalIgnoreCase) && vehicle is Suv) ||
                 (type.Equals("Truck", StringComparison.OrdinalIgnoreCase) && vehicle is Truck)) &&
                (manufacturer == null || vehicle?.GetManufacturer()?.Equals(manufacturer, StringComparison.OrdinalIgnoreCase) == true) &&
                (model == null || vehicle?.GetModel()?.Equals(model, StringComparison.OrdinalIgnoreCase) == true) &&
                (startYear == null || vehicle?.GetYear() >= startYear) &&
                (endYear == null || vehicle?.GetYear() <= endYear) &&
                (id == null || vehicle?.GetId() == id)
            ).ToList();

        if (filteredVehicles.Count == 0)
            throw new CustomExceptions.NoVehiclesFoundWithFiltersException();

        return filteredVehicles;
    }

    private void VehicleValidations(Vehicle? vehicle, List<Vehicle?>? existingVehicles, bool isUpdate = false)
    {
        if (vehicle == null)
            throw new CustomExceptions.VehicleDataNullException();

        if (existingVehicles != null && existingVehicles.Any(existingVehicle => existingVehicle?.GetId() == vehicle.GetId()) && !isUpdate)
            throw new CustomExceptions.VehicleAlreadyExistsException(vehicle.GetId());

        if (existingVehicles != null && existingVehicles.All(existingVehicle => existingVehicle?.GetId() != vehicle.GetId()) && isUpdate)
            throw new CustomExceptions.VehicleNotFoundException(vehicle.GetId());
    }

    private static Vehicle? UpdateVehicleType(Vehicle? vehicle, List<Vehicle?> existingVehicles)
    {
        var existingVehicle = existingVehicles.FirstOrDefault(v => v?.GetId() == vehicle?.GetId());
        if (existingVehicle?.GetType() == vehicle?.GetType())
            return vehicle;

        Vehicle newVehicle = vehicle switch
        {
            Sedan => new Sedan(
                vehicle.GetId(),
                vehicle.GetManufacturer(),
                vehicle.GetModel(),
                vehicle.GetYear(),
                vehicle.GetStartingBid(),
                vehicle is Sedan updatedSedan ? updatedSedan.GetNumberOfDoors() : 0),
            
            Hatchback => new Hatchback(vehicle.GetId(),
                vehicle.GetManufacturer(),
                vehicle.GetModel(),
                vehicle.GetYear(),
                vehicle.GetStartingBid(),
                vehicle is Hatchback updatedHatchback ? updatedHatchback.GetNumberOfDoors() : 0),
            
            Suv => new Suv(
                vehicle.GetId(),
                vehicle.GetManufacturer(),
                vehicle.GetModel(),
                vehicle.GetYear(),
                vehicle.GetStartingBid(),
                vehicle is Suv updatedSuv ? updatedSuv.GetNumberOfSeats() : 0),
            
            Truck => new Truck(
                vehicle.GetId(),
                vehicle.GetManufacturer(),
                vehicle.GetModel(),
                vehicle.GetYear(),
                vehicle.GetStartingBid(),
                vehicle is Truck updatedTruck ? updatedTruck.GetLoadCapacity() : 0),
            
            _ => throw new CustomExceptions.InvalidVehicleTypeException()
        };


        return newVehicle;
    }
}