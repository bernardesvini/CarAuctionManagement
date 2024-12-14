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

    public VehicleResponseDto? AddVehicle(VehicleRequestDto vehicleRequestDto)
    {
        var validator = new VehicleRequestDto.VehicleRequestDtoValidator();
        var validationResult = validator.Validate(vehicleRequestDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        switch (vehicleRequestDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                Hatchback hatchback = new Hatchback(
                    Guid.NewGuid(),
                    vehicleRequestDto.Manufacturer,
                    vehicleRequestDto.Model,
                    vehicleRequestDto.Year,
                    vehicleRequestDto.StartingBid,
                    vehicleRequestDto.NumberOfDoors
                );

                Vehicle? responseHatchback = _vehiclesService.AddVehicle(hatchback);

                VehicleResponseDto? responseHatchDto = responseHatchback?.ToResponseDto();
                return responseHatchDto;

            case DTOs.Enums.VehicleType.Sedan:
                Sedan sedan = new Sedan(
                    Guid.NewGuid(),
                    vehicleRequestDto.Manufacturer,
                    vehicleRequestDto.Model,
                    vehicleRequestDto.Year,
                    vehicleRequestDto.StartingBid,
                    vehicleRequestDto.NumberOfDoors
                );
                Vehicle? responseSedan = _vehiclesService.AddVehicle(sedan);
                VehicleResponseDto? responseSedanDto = responseSedan?.ToResponseDto();

                return responseSedanDto;

            case DTOs.Enums.VehicleType.Suv:
                Suv suv = new Suv(
                    Guid.NewGuid(),
                    vehicleRequestDto.Manufacturer,
                    vehicleRequestDto.Model,
                    vehicleRequestDto.Year,
                    vehicleRequestDto.StartingBid,
                    vehicleRequestDto.NumberOfSeats
                );
                Vehicle? responseSuv = _vehiclesService.AddVehicle(suv);
                VehicleResponseDto? responseSuvDto = responseSuv?.ToResponseDto();

                return responseSuvDto;

            case DTOs.Enums.VehicleType.Truck:
                Truck truck = new Truck(
                    Guid.NewGuid(),
                    vehicleRequestDto.Manufacturer,
                    vehicleRequestDto.Model,
                    vehicleRequestDto.Year,
                    vehicleRequestDto.StartingBid,
                    vehicleRequestDto.LoadCapacity
                );
                Vehicle? responseTruck = _vehiclesService.AddVehicle(truck);
                VehicleResponseDto? responseTruckDto = responseTruck?.ToResponseDto();

                return responseTruckDto;

            default:
                return new VehicleResponseDto();
        }
    }

    public VehicleResponseDto? UpdateVehicle(VehicleUpdateRequestDto vehicleUpdateRequestDto)
    {
        var validator = new VehicleUpdateRequestDto.VehicleUpdateDtoValidator();
        var validationResult = validator.Validate(vehicleUpdateRequestDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        switch (vehicleUpdateRequestDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                Hatchback hatchback = new Hatchback(
                    Guid.NewGuid(),
                    vehicleUpdateRequestDto.Manufacturer,
                    vehicleUpdateRequestDto.Model,
                    vehicleUpdateRequestDto.Year,
                    vehicleUpdateRequestDto.StartingBid,
                    vehicleUpdateRequestDto.NumberOfDoors
                );

                Vehicle? responseHatchback = _vehiclesService.UpdateVehicle(hatchback);

                VehicleResponseDto? responseHatchDto = responseHatchback?.ToResponseDto();
                return responseHatchDto;

            case DTOs.Enums.VehicleType.Sedan:
                Sedan sedan = new Sedan(
                    Guid.NewGuid(),
                    vehicleUpdateRequestDto.Manufacturer,
                    vehicleUpdateRequestDto.Model,
                    vehicleUpdateRequestDto.Year,
                    vehicleUpdateRequestDto.StartingBid,
                    vehicleUpdateRequestDto.NumberOfDoors
                );
                Vehicle? responseSedan = _vehiclesService.UpdateVehicle(sedan);
                VehicleResponseDto? responseSedanDto = responseSedan?.ToResponseDto();

                return responseSedanDto;

            case DTOs.Enums.VehicleType.Suv:
                Suv suv = new Suv(
                    Guid.NewGuid(),
                    vehicleUpdateRequestDto.Manufacturer,
                    vehicleUpdateRequestDto.Model,
                    vehicleUpdateRequestDto.Year,
                    vehicleUpdateRequestDto.StartingBid,
                    vehicleUpdateRequestDto.NumberOfSeats
                );
                Vehicle? responseSuv = _vehiclesService.UpdateVehicle(suv);
                VehicleResponseDto? responseSuvDto = responseSuv?.ToResponseDto();

                return responseSuvDto;

            case DTOs.Enums.VehicleType.Truck:
                Truck truck = new Truck(
                    Guid.NewGuid(),
                    vehicleUpdateRequestDto.Manufacturer,
                    vehicleUpdateRequestDto.Model,
                    vehicleUpdateRequestDto.Year,
                    vehicleUpdateRequestDto.StartingBid,
                    vehicleUpdateRequestDto.LoadCapacity
                );
                Vehicle? responseTruck = _vehiclesService.UpdateVehicle(truck);
                VehicleResponseDto? responseTruckDto = responseTruck?.ToResponseDto();

                return responseTruckDto;

            default:
                return new VehicleResponseDto();
        }
    }
}