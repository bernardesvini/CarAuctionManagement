using CarAuctionManagement.DTOs.Vehicles.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public sealed class Truck : Vehicle
{
    private decimal? LoadCapacity { get; set; }
    
    public Truck(Guid? id, string? manufacturer, string? model, int? year, decimal? startingBid, decimal? loadCapacity)
        : base(id, manufacturer, model, year, startingBid)
    {
        LoadCapacity = loadCapacity;
        Validate();
    }
    
    public override VehicleResponseDto ToResponseDto()
    {
        return new VehicleResponseDto
        {
            Id = GetId(),
            Manufacturer = GetManufacturer(),
            Model = GetModel(),
            Year = GetYear(),
            StartingBid = GetStartingBid(),
            Type = DTOs.Enums.VehicleType.Truck,
            LoadCapacity = LoadCapacity
        };
    }
    
    public decimal? GetLoadCapacity() => LoadCapacity;
    
    public override void Validate()
    {
        base.Validate();

        if (LoadCapacity <= 0)
            throw new CustomExceptions.ValidationException("Truck load capacity must be greater than 0.");

        if (LoadCapacity > 1000000) 
            throw new CustomExceptions.ValidationException("Truck load capacity exceeds maximum limit.");
    }
}