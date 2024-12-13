using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Vehicles.RemoveVehicle;

public class RemoveVehicleHandler : IRemoveVehicleHandler
{
    
    private readonly IVehiclesService _vehiclesService;
    
    public RemoveVehicleHandler(IVehiclesService vehiclesService)
    {
        _vehiclesService = vehiclesService;
    }
    
    public void RemoveVehicle(VehicleRemoveRequestDto vehicle)
    {
        var validator = new VehicleRemoveRequestDto.VehicleRemoveRequestDtoValidator();
        var validationResult = validator.Validate(vehicle);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        _vehiclesService.RemoveVehicle(vehicle.Id);   
    }
}