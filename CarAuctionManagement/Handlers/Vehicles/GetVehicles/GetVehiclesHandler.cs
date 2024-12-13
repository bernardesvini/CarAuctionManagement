using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.DTOs.Vehicles.Responses;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Vehicles.GetVehicles;

public class GetVehiclesHandler : IGetVehiclesHandler
{
    private readonly IVehiclesService _vehiclesService;
    
    public GetVehiclesHandler(IVehiclesService vehiclesService)
    {
        _vehiclesService = vehiclesService;
    }
    
    public GetVehicleshResponseDto GetVehicles()
    {
        List<Vehicle?> vehicles = _vehiclesService.GetVehicles();
        var response = GenerateResponseByType(vehicles);
        return response;
    }

    public GetVehicleshResponseDto GetVehiclesWithFilters(int? yearFilter, string? typeFilter, string? manufacturerFilter, string? modelFilter)
    {
        var filters = new VehicleSearchRequestDto
        {
            Year = yearFilter,
            Type = typeFilter,
            Manufacturer = manufacturerFilter,
            Model = modelFilter
        };
        
        var validator = new VehicleSearchRequestDto.VehicleSearchRequestDtoValidator();
        var validationResult = validator.Validate(filters);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        string? type = !string.IsNullOrEmpty(filters.Type) ? filters.Type : null;
        string? manufacturer = !string.IsNullOrEmpty(filters.Manufacturer) ? filters.Manufacturer : null;
        string? model = !string.IsNullOrEmpty(filters.Model) ? filters.Model : null;
        int? year = filters.Year == 0 ? null : filters.Year;
        
        
        List<Vehicle?>? vehicles = _vehiclesService.GetVehicleSearch(type, manufacturer, model, year);
        var response = GenerateResponseByType(vehicles);
        return response;
    }
    
    private static GetVehicleshResponseDto GenerateResponseByType(List<Vehicle?>? vehicles)
    {
        GetVehicleshResponseDto response = new GetVehicleshResponseDto();
        response.GetVehicles = new List<VehicleResponseDto>();
        if (vehicles != null)
            foreach (var vehicle in vehicles)
            {
                switch (vehicle)
                {
                    case Hatchback hatchback:
                        response.GetVehicles.Add(new VehicleResponseDto()
                        {
                            Id = hatchback.Id,
                            Manufacturer = hatchback.Manufacturer,
                            Model = hatchback.Model,
                            Year = hatchback.Year,
                            Type = DTOs.Enums.VehicleType.Hatchback,
                            StartingBid = hatchback.StartingBid,
                            NumberOfDoors = hatchback.NumberOfDoors
                        });
                        break;
                    case Sedan sedan:
                        response.GetVehicles.Add(new VehicleResponseDto()
                        {
                            Id = sedan.Id,
                            Manufacturer = sedan.Manufacturer,
                            Model = sedan.Model,
                            Year = sedan.Year,
                            Type = DTOs.Enums.VehicleType.Sedan,
                            StartingBid = sedan.StartingBid,
                            NumberOfDoors = sedan.NumberOfDoors
                        });
                        break;
                    case Suv suv:
                        response.GetVehicles.Add(new VehicleResponseDto()
                        {
                            Id = suv.Id,
                            Manufacturer = suv.Manufacturer,
                            Model = suv.Model,
                            Year = suv.Year,
                            Type = DTOs.Enums.VehicleType.Suv,
                            StartingBid = suv.StartingBid,
                            NumberOfSeats = suv.NumberOfSeats
                        });
                        break;
                    case Truck truck:
                        response.GetVehicles.Add(new VehicleResponseDto()
                        {
                            Id = truck.Id,
                            Manufacturer = truck.Manufacturer,
                            Model = truck.Model,
                            Year = truck.Year,
                            Type = DTOs.Enums.VehicleType.Truck,
                            StartingBid = truck.StartingBid,
                            LoadCapacity = truck.LoadCapacity
                        });
                        break;
                }
            }

        return response;
    }
}