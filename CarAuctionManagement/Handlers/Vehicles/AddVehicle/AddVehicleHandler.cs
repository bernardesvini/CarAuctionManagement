using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.DTOs.Vehicles.Responses;
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
    
    public VehicleResponseDto AddVehicle(VehicleRequestDto vehicleRequestDto)
    {
        var validator = new VehicleRequestDto.VehicleRequestDtoValidator();
        var validationResult = validator.Validate(vehicleRequestDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        switch(vehicleRequestDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                Hatchback hatchback = new Hatchback()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleRequestDto.Manufacturer,
                    Model = vehicleRequestDto.Model,
                    Year = vehicleRequestDto.Year,
                    StartingBid = vehicleRequestDto.StartingBid,
                    NumberOfDoors = vehicleRequestDto.NumberOfDoors
                };
                Vehicle responseHatchback = _vehiclesService.AddVehicle(hatchback);
                VehicleResponseDto responseHatchDto = new VehicleResponseDto
                {
                    Id = responseHatchback.Id,
                    Manufacturer = responseHatchback.Manufacturer,
                    Model = responseHatchback.Model,
                    Year = responseHatchback.Year,
                    StartingBid = responseHatchback.StartingBid,
                    Type = DTOs.Enums.VehicleType.Hatchback,
                    NumberOfDoors = ((Hatchback)responseHatchback).NumberOfDoors,
                    NumberOfSeats = 0,
                    LoadCapacity = 0
                };
                return responseHatchDto;
            
            case DTOs.Enums.VehicleType.Sedan:
                Sedan sedan = new Sedan()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleRequestDto.Manufacturer,
                    Model = vehicleRequestDto.Model,
                    Year = vehicleRequestDto.Year,
                    StartingBid = vehicleRequestDto.StartingBid,
                    NumberOfDoors = vehicleRequestDto.NumberOfDoors
                };
                Vehicle responseSedan = _vehiclesService.AddVehicle(sedan);
                VehicleResponseDto responseSedanDto = new VehicleResponseDto
                {
                    Id = responseSedan.Id,
                    Manufacturer = responseSedan.Manufacturer,
                    Model = responseSedan.Model,
                    Year = responseSedan.Year,
                    StartingBid = responseSedan.StartingBid,
                    Type = DTOs.Enums.VehicleType.Sedan,
                    NumberOfDoors = ((Sedan)responseSedan).NumberOfDoors,
                    NumberOfSeats = 0,
                    LoadCapacity = 0
                };
                return responseSedanDto;
            
            case DTOs.Enums.VehicleType.Suv:
                Suv suv = new Suv()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleRequestDto.Manufacturer,
                    Model = vehicleRequestDto.Model,
                    Year = vehicleRequestDto.Year,
                    StartingBid = vehicleRequestDto.StartingBid,
                    NumberOfSeats = vehicleRequestDto.NumberOfSeats
                };
                Vehicle responseSuv = _vehiclesService.AddVehicle(suv);
                VehicleResponseDto responseSuvDto = new VehicleResponseDto
                {
                    Id = responseSuv.Id,
                    Manufacturer = responseSuv.Manufacturer,
                    Model = responseSuv.Model,
                    Year = responseSuv.Year,
                    StartingBid = responseSuv.StartingBid,
                    Type = DTOs.Enums.VehicleType.Suv,
                    NumberOfSeats = ((Suv)responseSuv).NumberOfSeats,
                    NumberOfDoors = 0,
                    LoadCapacity = 0
                };
                return responseSuvDto;
            
            case DTOs.Enums.VehicleType.Truck:
                Truck truck = new Truck()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = vehicleRequestDto.Manufacturer,
                    Model = vehicleRequestDto.Model,
                    Year = vehicleRequestDto.Year,
                    StartingBid = vehicleRequestDto.StartingBid,
                    LoadCapacity = vehicleRequestDto.LoadCapacity
                };
                Vehicle responseTruck = _vehiclesService.AddVehicle(truck);
                VehicleResponseDto responseTruckDto = new VehicleResponseDto
                {
                    Id = responseTruck.Id,
                    Manufacturer = responseTruck.Manufacturer,
                    Model = responseTruck.Model,
                    Year = responseTruck.Year,
                    StartingBid = responseTruck.StartingBid,
                    Type = DTOs.Enums.VehicleType.Truck,
                    LoadCapacity = ((Truck)responseTruck).LoadCapacity,
                    NumberOfDoors = 0,
                    NumberOfSeats = 0
                };
                return responseTruckDto;
            
            default:
                return new VehicleResponseDto();
        }
    }

    public VehicleResponseDto UpdateVehicle(VehicleUpdateRequestDto vehicleUpdateRequestDto)
    {
       var validator = new VehicleUpdateRequestDto.VehicleUpdateDtoValidator();
        var validationResult = validator.Validate(vehicleUpdateRequestDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        switch(vehicleUpdateRequestDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                Hatchback hatchback = new Hatchback()
                {
                    Id = vehicleUpdateRequestDto.Id,
                    Manufacturer = vehicleUpdateRequestDto.Manufacturer,
                    Model = vehicleUpdateRequestDto.Model,
                    Year = vehicleUpdateRequestDto.Year,
                    StartingBid = vehicleUpdateRequestDto.StartingBid,
                    NumberOfDoors = vehicleUpdateRequestDto.NumberOfDoors
                };
                Vehicle responseHatchback = _vehiclesService.UpdateVehicle(hatchback);
                VehicleResponseDto responseHatchDto = new VehicleResponseDto
                {
                    Id = responseHatchback.Id,
                    Manufacturer = responseHatchback.Manufacturer,
                    Model = responseHatchback.Model,
                    Year = responseHatchback.Year,
                    StartingBid = responseHatchback.StartingBid,
                    Type = DTOs.Enums.VehicleType.Hatchback,
                    NumberOfDoors = ((Hatchback)responseHatchback).NumberOfDoors,
                    NumberOfSeats = 0,
                    LoadCapacity = 0
                };
                return responseHatchDto;
            
            case DTOs.Enums.VehicleType.Sedan:
                Sedan sedan = new Sedan()
                {
                    Id = vehicleUpdateRequestDto.Id,
                    Manufacturer = vehicleUpdateRequestDto.Manufacturer,
                    Model = vehicleUpdateRequestDto.Model,
                    Year = vehicleUpdateRequestDto.Year,
                    StartingBid = vehicleUpdateRequestDto.StartingBid,
                    NumberOfDoors = vehicleUpdateRequestDto.NumberOfDoors
                };
                Vehicle responseSedan = _vehiclesService.UpdateVehicle(sedan);
                VehicleResponseDto responseSedanDto = new VehicleResponseDto
                {
                    Id = responseSedan.Id,
                    Manufacturer = responseSedan.Manufacturer,
                    Model = responseSedan.Model,
                    Year = responseSedan.Year,
                    StartingBid = responseSedan.StartingBid,
                    Type = DTOs.Enums.VehicleType.Sedan,
                    NumberOfDoors = ((Sedan)responseSedan).NumberOfDoors,
                    NumberOfSeats = 0,
                    LoadCapacity = 0
                };
                return responseSedanDto;
            
            case DTOs.Enums.VehicleType.Suv:
                Suv suv = new Suv()
                {
                    Id = vehicleUpdateRequestDto.Id,
                    Manufacturer = vehicleUpdateRequestDto.Manufacturer,
                    Model = vehicleUpdateRequestDto.Model,
                    Year = vehicleUpdateRequestDto.Year,
                    StartingBid = vehicleUpdateRequestDto.StartingBid,
                    NumberOfSeats = vehicleUpdateRequestDto.NumberOfSeats
                };
                Vehicle responseSuv = _vehiclesService.UpdateVehicle(suv);
                VehicleResponseDto responseSuvDto = new VehicleResponseDto
                {
                    Id = responseSuv.Id,
                    Manufacturer = responseSuv.Manufacturer,
                    Model = responseSuv.Model,
                    Year = responseSuv.Year,
                    StartingBid = responseSuv.StartingBid,
                    Type = DTOs.Enums.VehicleType.Suv,
                    NumberOfSeats = ((Suv)responseSuv).NumberOfSeats,
                    NumberOfDoors = 0,
                    LoadCapacity = 0
                };
                return responseSuvDto;
            
            case DTOs.Enums.VehicleType.Truck:
                Truck truck = new Truck()
                {
                    Id = vehicleUpdateRequestDto.Id,
                    Manufacturer = vehicleUpdateRequestDto.Manufacturer,
                    Model = vehicleUpdateRequestDto.Model,
                    Year = vehicleUpdateRequestDto.Year,
                    StartingBid = vehicleUpdateRequestDto.StartingBid,
                    LoadCapacity = vehicleUpdateRequestDto.LoadCapacity
                };
                Vehicle responseTruck = _vehiclesService.UpdateVehicle(truck);
                VehicleResponseDto responseTruckDto = new VehicleResponseDto
                {
                    Id = responseTruck.Id,
                    Manufacturer = responseTruck.Manufacturer,
                    Model = responseTruck.Model,
                    Year = responseTruck.Year,
                    StartingBid = responseTruck.StartingBid,
                    Type = DTOs.Enums.VehicleType.Truck,
                    LoadCapacity = ((Truck)responseTruck).LoadCapacity,
                    NumberOfDoors = 0,
                    NumberOfSeats = 0
                };
                return responseTruckDto;
            
            default:
                return new VehicleResponseDto();
        }
    }
}