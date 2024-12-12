using CarAuctionManagement.DTOs.Vehicles;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Vehicles.AddVehicle;

public class AddVehicleHandler : IAddVehicleHandler
{
    private readonly IVehiclesService _vehiclesService;
    
    public AddVehicleHandler(IVehiclesService vehiclesService)
    {
        _vehiclesService = vehiclesService;
    }
    
    public Vehicle AddVehicle(VehicleDto vehicleDto)
    {
        var validator = new VehicleDto.VehicleDtoValidator();
        var validationResult = validator.Validate(vehicleDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        switch(vehicleDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                Hatchback hatchback = new Hatchback()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleDto.Manufacturer,
                    Model = vehicleDto.Model,
                    Year = vehicleDto.Year,
                    StartingBid = vehicleDto.StartingBid,
                    NumberOfDoors = vehicleDto.NumberOfDoors
                };
                Vehicle responseHatchback = _vehiclesService.AddVehicle(hatchback);
                return responseHatchback;
            
            case DTOs.Enums.VehicleType.Sedan:
                Sedan sedan = new Sedan()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleDto.Manufacturer,
                    Model = vehicleDto.Model,
                    Year = vehicleDto.Year,
                    StartingBid = vehicleDto.StartingBid,
                    NumberOfDoors = vehicleDto.NumberOfDoors
                };
                Vehicle responseSedan = _vehiclesService.AddVehicle(sedan);
                return responseSedan;
            
            case DTOs.Enums.VehicleType.Suv:
                Suv suv = new Suv()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleDto.Manufacturer,
                    Model = vehicleDto.Model,
                    Year = vehicleDto.Year,
                    StartingBid = vehicleDto.StartingBid,
                    NumberOfSeats = vehicleDto.NumberOfSeats
                };
                Vehicle responseSuv = _vehiclesService.AddVehicle(suv);
                return responseSuv;
            
            case DTOs.Enums.VehicleType.Truck:
                Truck truck = new Truck()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleDto.Manufacturer,
                    Model = vehicleDto.Model,
                    Year = vehicleDto.Year,
                    StartingBid = vehicleDto.StartingBid,
                    LoadCapacity = vehicleDto.LoadCapacity
                };
                Vehicle responseTruck = _vehiclesService.AddVehicle(truck);
                return responseTruck;
            
            default:
                return new Vehicle();
        }
    }
}