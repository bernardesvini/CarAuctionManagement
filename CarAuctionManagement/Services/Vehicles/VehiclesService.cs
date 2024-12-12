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

    public Vehicle AddVehicle(Vehicle vehicle)
    {
        vehicle.Validate();
        List<Vehicle?> existingVehicles = _vehiclesRepository.GetVehicles();
        VehicleValidations(vehicle, existingVehicles);
        var response =  _vehiclesRepository.AddVehicle(vehicle);
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

    public List<Vehicle?> GetVehicleSearch(string? type = null, string? manufacturer = null, string? model = null, int? year = null)
    {
        return ApplyFilterOptions(type, manufacturer, model, year);
    }

    public void UpdateVehicle(Vehicle? vehicle)
    {
        vehicle?.Validate();
        List<Vehicle?> existingVehicles = _vehiclesRepository.GetVehicles();
        VehicleValidations(vehicle, existingVehicles, true);
        Vehicle? vehicleToUpdate = UpdateVehicleType(vehicle, existingVehicles);
        _vehiclesRepository.UpdateVehicle(vehicleToUpdate);
    }

    public void RemoveVehicle(Guid? id)
    {
        Vehicle vehicleToRemoved = GetVehicleByIdValidation(id);
        _vehiclesRepository.RemoveVehicle(vehicleToRemoved);
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

        Vehicle? vehicle = allVehicles.FirstOrDefault(vehicle => vehicle?.Id == id);

        if (vehicle == null)
            throw new CustomExceptions.VehicleNotFoundException(id);

        return vehicle;
    }

    private List<Vehicle?> ApplyFilterOptions(string? type = null, string? manufacturer = null, string? model = null, int? year = null)
    {
        List<Vehicle?> vehicles = _vehiclesRepository.GetVehicles();
        List<Vehicle?> filteredVehicles = vehicles
            .Where(vehicle =>
                (type == null ||
                 (type.Equals("Sedan", StringComparison.OrdinalIgnoreCase) && vehicle is Sedan) ||
                 (type.Equals("Hatchback", StringComparison.OrdinalIgnoreCase) && vehicle is Hatchback) ||
                 (type.Equals("SUV", StringComparison.OrdinalIgnoreCase) && vehicle is Suv) ||
                 (type.Equals("Truck", StringComparison.OrdinalIgnoreCase) && vehicle is Truck)) &&
                (manufacturer == null || vehicle?.Manufacturer?.Equals(manufacturer, StringComparison.OrdinalIgnoreCase) == true) &&
                (model == null || vehicle?.Model?.Equals(model, StringComparison.OrdinalIgnoreCase) == true) &&
                (year == null || vehicle?.Year == year)
            ).ToList();

        if (filteredVehicles.Count == 0)
            throw new CustomExceptions.NoVehiclesFoundWithFiltersException();

        return filteredVehicles;
    }

    private void VehicleValidations(Vehicle? vehicle, List<Vehicle?>? existingVehicles, bool isUpdate = false)
    {
        if (vehicle == null)
            throw new CustomExceptions.VehicleDataNullException();

        if (existingVehicles != null && existingVehicles.Any(existingVehicle => existingVehicle?.Id == vehicle.Id) && !isUpdate)
            throw new CustomExceptions.VehicleAlreadyExistsException(vehicle.Id);

        if (existingVehicles != null && existingVehicles.All(existingVehicle => existingVehicle?.Id != vehicle.Id) && isUpdate)
            throw new CustomExceptions.VehicleNotFoundException(vehicle.Id);
    }

    private static Vehicle? UpdateVehicleType(Vehicle? vehicle, List<Vehicle?> existingVehicles)
    {
        var existingVehicle = existingVehicles.FirstOrDefault(v => v?.Id == vehicle?.Id);
        if (existingVehicle?.GetType() == vehicle?.GetType())
            return vehicle;

        Vehicle newVehicle = vehicle switch
        {
            Sedan => new Sedan(),
            Hatchback => new Hatchback(),
            Suv => new Suv(),
            Truck => new Truck(),
            _ => throw new CustomExceptions.InvalidVehicleTypeException()
        };

        newVehicle.Id = vehicle.Id;
        newVehicle.Manufacturer = vehicle.Manufacturer;
        newVehicle.Model = vehicle.Model;
        newVehicle.Year = vehicle.Year;
        newVehicle.StartingBid = vehicle.StartingBid;

        switch (newVehicle)
        {
            case Hatchback newHatchback when vehicle is Hatchback updatedHatchback:
                newHatchback.NumberOfDoors = updatedHatchback.NumberOfDoors;
                break;
            case Sedan newSedan when vehicle is Sedan updatedSedan:
                newSedan.NumberOfDoors = updatedSedan.NumberOfDoors;
                break;
            case Suv newSuv when vehicle is Suv updatedSuv:
                newSuv.NumberOfSeats = updatedSuv.NumberOfSeats;
                break;
            case Truck newTruck when vehicle is Truck updatedTruck:
                newTruck.LoadCapacity = updatedTruck.LoadCapacity;
                break;
        }

        return newVehicle;
    }
}