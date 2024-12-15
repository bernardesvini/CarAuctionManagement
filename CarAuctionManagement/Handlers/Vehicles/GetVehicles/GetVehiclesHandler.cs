using CarAuctionManagement.DTOs.Enums;
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

    public GetVehiclesResponseDto GetVehiclesWithFilters(int? startYearFilter, int? endYearFilter, Guid? idFilter, VehicleType? typeFilter, string? manufacturerFilter, string? modelFilter, int page, int pageSize)
    {
        var filters = new VehicleSearchRequestDto
        {
            StartYear = startYearFilter,
            EndYear = endYearFilter,
            Id = idFilter,
            Type = typeFilter.ToString(),
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
        int? startYear = filters.StartYear == 0 ? null : filters.StartYear;
        int? endYear = filters.EndYear == 0 ? null : filters.EndYear;
        Guid? id = filters.Id == Guid.Empty ? null : filters.Id;
        
        
        List<Vehicle?>? vehicles = _vehiclesService.GetVehicleSearch(type, manufacturer, model, startYear, endYear, id);
        var responseByType = GenerateResponseByType(vehicles);
        
        var paginatedResponse = responseByType?.VehiclesList?
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)             
            .ToList();

        
        var response = new GetVehiclesResponseDto
        {
            TotalCount = vehicles?.Count,
            Page = page,
            PageSize = pageSize,
            VehiclesList = paginatedResponse
        };
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