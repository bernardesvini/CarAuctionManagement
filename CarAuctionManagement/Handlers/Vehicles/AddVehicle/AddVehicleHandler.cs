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
                var hatchback = CreateHatchback(vehicleRequestDto);
                Vehicle? responseHatchback = _vehiclesService.AddVehicle(hatchback);
                VehicleResponseDto? responseHatchDto = responseHatchback?.ToResponseDto();
                return responseHatchDto;

            case DTOs.Enums.VehicleType.Sedan:
                var sedan = CreateSedan(vehicleRequestDto);
                Vehicle? responseSedan = _vehiclesService.AddVehicle(sedan);
                VehicleResponseDto? responseSedanDto = responseSedan?.ToResponseDto();
                return responseSedanDto;

            case DTOs.Enums.VehicleType.Suv:
                var suv = CreateSuv(vehicleRequestDto);
                Vehicle? responseSuv = _vehiclesService.AddVehicle(suv);
                VehicleResponseDto? responseSuvDto = responseSuv?.ToResponseDto();
                return responseSuvDto;

            case DTOs.Enums.VehicleType.Truck:
                var truck = CreateTruck(vehicleRequestDto);
                Vehicle? responseTruck = _vehiclesService.AddVehicle(truck);
                VehicleResponseDto? responseTruckDto = responseTruck?.ToResponseDto();
                return responseTruckDto;

            default:
                return new VehicleResponseDto();
        }
    }

    public VehicleResponseDto? UpdateVehicle(Guid id, VehicleUpdateRequestDto vehicleUpdateRequestDto)
    {
        Vehicle? existingVehicle = _vehiclesService.GetVehicleById(id);

        switch (vehicleUpdateRequestDto.Type)
        {
            case DTOs.Enums.VehicleType.Hatchback:
                var hatchback = UpdateHatchback(id, vehicleUpdateRequestDto, existingVehicle);
                Vehicle? responseHatchback = _vehiclesService.UpdateVehicle(hatchback);
                VehicleResponseDto? responseHatchDto = responseHatchback?.ToResponseDto();
                return responseHatchDto;

            case DTOs.Enums.VehicleType.Sedan:
                var sedan = UpdateSedan(id, vehicleUpdateRequestDto, existingVehicle);
                Vehicle? responseSedan = _vehiclesService.UpdateVehicle(sedan);
                VehicleResponseDto? responseSedanDto = responseSedan?.ToResponseDto();
                return responseSedanDto;

            case DTOs.Enums.VehicleType.Suv:
                var suv = UpdateSuv(id, vehicleUpdateRequestDto, existingVehicle);
                Vehicle? responseSuv = _vehiclesService.UpdateVehicle(suv);
                VehicleResponseDto? responseSuvDto = responseSuv?.ToResponseDto();
                return responseSuvDto;

            case DTOs.Enums.VehicleType.Truck:
                var truck = UpdateTruck(id, vehicleUpdateRequestDto, existingVehicle);
                Vehicle? responseTruck = _vehiclesService.UpdateVehicle(truck);
                VehicleResponseDto? responseTruckDto = responseTruck?.ToResponseDto();

                return responseTruckDto;

            default:
                return new VehicleResponseDto();
        }
    }

    private Truck UpdateTruck(Guid id, VehicleUpdateRequestDto vehicleUpdateRequestDto, Vehicle? existingVehicle)
    {
        Truck truck = new Truck(
            id,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Manufacturer) ? existingVehicle?.GetManufacturer() : vehicleUpdateRequestDto.Manufacturer,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Model) ? existingVehicle?.GetModel() : vehicleUpdateRequestDto.Model,
            vehicleUpdateRequestDto.Year == null || !IsValidYear(vehicleUpdateRequestDto.Year) ? existingVehicle?.GetYear() : vehicleUpdateRequestDto.Year,
            vehicleUpdateRequestDto.StartingBid == 0 || vehicleUpdateRequestDto.StartingBid == null ? existingVehicle?.GetStartingBid() : vehicleUpdateRequestDto.StartingBid,
            vehicleUpdateRequestDto.LoadCapacity == null || vehicleUpdateRequestDto.LoadCapacity <= 0 || vehicleUpdateRequestDto.LoadCapacity > 1000000
                ? existingVehicle is Truck ? ((Truck)existingVehicle).GetLoadCapacity() : 0
                : vehicleUpdateRequestDto.LoadCapacity
        );
        return truck;
    }

    private Suv UpdateSuv(Guid id, VehicleUpdateRequestDto vehicleUpdateRequestDto, Vehicle? existingVehicle)
    {
        Suv suv = new Suv(
            id,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Manufacturer) ? existingVehicle?.GetManufacturer() : vehicleUpdateRequestDto.Manufacturer,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Model) ? existingVehicle?.GetModel() : vehicleUpdateRequestDto.Model,
            vehicleUpdateRequestDto.Year == null || !IsValidYear(vehicleUpdateRequestDto.Year) ? existingVehicle?.GetYear() : vehicleUpdateRequestDto.Year,
            vehicleUpdateRequestDto.StartingBid == 0 || vehicleUpdateRequestDto.StartingBid == null ? existingVehicle?.GetStartingBid() : vehicleUpdateRequestDto.StartingBid,
            vehicleUpdateRequestDto.NumberOfSeats == null || vehicleUpdateRequestDto.NumberOfSeats < 2
                ? existingVehicle is Suv ? ((Suv)existingVehicle).GetNumberOfSeats() : 0
                : vehicleUpdateRequestDto.NumberOfSeats
        );
        return suv;
    }

    private Sedan UpdateSedan(Guid id, VehicleUpdateRequestDto vehicleUpdateRequestDto, Vehicle? existingVehicle)
    {
        Sedan sedan = new Sedan(
            id,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Manufacturer) ? existingVehicle?.GetManufacturer() : vehicleUpdateRequestDto.Manufacturer,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Model) ? existingVehicle?.GetModel() : vehicleUpdateRequestDto.Model,
            vehicleUpdateRequestDto.Year == null || !IsValidYear(vehicleUpdateRequestDto.Year) ? existingVehicle?.GetYear() : vehicleUpdateRequestDto.Year,
            vehicleUpdateRequestDto.StartingBid == 0 || vehicleUpdateRequestDto.StartingBid == null ? existingVehicle?.GetStartingBid() : vehicleUpdateRequestDto.StartingBid,
            vehicleUpdateRequestDto.NumberOfDoors == null || vehicleUpdateRequestDto.NumberOfDoors < 2 || vehicleUpdateRequestDto.NumberOfDoors > 5
                ? existingVehicle is Sedan ? ((Sedan)existingVehicle).GetNumberOfDoors() : 0
                : vehicleUpdateRequestDto.NumberOfDoors
        );
        return sedan;
    }

    private Hatchback UpdateHatchback(Guid id, VehicleUpdateRequestDto vehicleUpdateRequestDto, Vehicle? existingVehicle)
    {
        Hatchback hatchback = new Hatchback(
            id,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Manufacturer) ? existingVehicle?.GetManufacturer() : vehicleUpdateRequestDto.Manufacturer,
            string.IsNullOrEmpty(vehicleUpdateRequestDto.Model) ? existingVehicle?.GetModel() : vehicleUpdateRequestDto.Model,
            vehicleUpdateRequestDto.Year == null || !IsValidYear(vehicleUpdateRequestDto.Year) ? existingVehicle?.GetYear() : vehicleUpdateRequestDto.Year,
            vehicleUpdateRequestDto.StartingBid == 0 || vehicleUpdateRequestDto.StartingBid == null ? existingVehicle?.GetStartingBid() : vehicleUpdateRequestDto.StartingBid,
            vehicleUpdateRequestDto.NumberOfDoors == null || vehicleUpdateRequestDto.NumberOfDoors < 2 || vehicleUpdateRequestDto.NumberOfDoors > 5
                ? existingVehicle is Hatchback ? ((Hatchback)existingVehicle).GetNumberOfDoors() : 0
                : vehicleUpdateRequestDto.NumberOfDoors
        );
        return hatchback;
    }

    private static Truck CreateTruck(VehicleRequestDto vehicleRequestDto)
    {
        Truck truck = new Truck(
            Guid.NewGuid(),
            vehicleRequestDto.Manufacturer,
            vehicleRequestDto.Model,
            vehicleRequestDto.Year,
            vehicleRequestDto.StartingBid,
            vehicleRequestDto.LoadCapacity
        );
        return truck;
    }

    private static Suv CreateSuv(VehicleRequestDto vehicleRequestDto)
    {
        Suv suv = new Suv(
            Guid.NewGuid(),
            vehicleRequestDto.Manufacturer,
            vehicleRequestDto.Model,
            vehicleRequestDto.Year,
            vehicleRequestDto.StartingBid,
            vehicleRequestDto.NumberOfSeats
        );
        return suv;
    }

    private static Sedan CreateSedan(VehicleRequestDto vehicleRequestDto)
    {
        Sedan sedan = new Sedan(
            Guid.NewGuid(),
            vehicleRequestDto.Manufacturer,
            vehicleRequestDto.Model,
            vehicleRequestDto.Year,
            vehicleRequestDto.StartingBid,
            vehicleRequestDto.NumberOfDoors
        );
        return sedan;
    }

    private static Hatchback CreateHatchback(VehicleRequestDto vehicleRequestDto)
    {
        Hatchback hatchback = new Hatchback(
            Guid.NewGuid(),
            vehicleRequestDto.Manufacturer,
            vehicleRequestDto.Model,
            vehicleRequestDto.Year,
            vehicleRequestDto.StartingBid,
            vehicleRequestDto.NumberOfDoors
        );
        return hatchback;
    }
    private bool IsValidYear(int? year)
    {
        return year >= 1886 && year <= DateTime.Now.Year;
    }
}