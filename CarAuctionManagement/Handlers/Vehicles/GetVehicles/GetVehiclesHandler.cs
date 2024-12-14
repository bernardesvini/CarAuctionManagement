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
    
    public GetVehiclesResponseDto GetVehicles()
    {
        List<Vehicle?>? vehicles = _vehiclesService.GetVehicles();
        var response = GenerateResponseByType(vehicles);
        return response;
    }

    public GetVehiclesResponseDto GetVehiclesWithFilters(int? yearFilter, string? typeFilter, string? manufacturerFilter, string? modelFilter)
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
    
    private static GetVehiclesResponseDto GenerateResponseByType(List<Vehicle?>? vehicles)
    {
        GetVehiclesResponseDto response = new GetVehiclesResponseDto();
        response.VehiclesList = new List<VehicleResponseDto?>();
        if (vehicles != null)
            foreach (var vehicle in vehicles)
            {
                switch (vehicle)
                {
                    case Hatchback hatchback:
                        response.VehiclesList.Add(hatchback.ToResponseDto());
                        break;
                    case Sedan sedan:
                        response.VehiclesList.Add(sedan.ToResponseDto());
                        break;
                    case Suv suv:
                        response.VehiclesList.Add(suv.ToResponseDto());
                        break;
                    case Truck truck:
                        response.VehiclesList.Add(truck.ToResponseDto());
                        break;
                }
            }

        return response;
    }
}