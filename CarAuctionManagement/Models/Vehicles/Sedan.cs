using CarAuctionManagement.DTOs.Vehicles.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public sealed class Sedan : Vehicle
{
    private int? NumberOfDoors { get; set; }
    
    public Sedan(Guid? id, string? manufacturer, string? model, int? year, decimal? startingBid, int? numberOfDoors)
        : base(id, manufacturer, model, year, startingBid)
    {
        NumberOfDoors = numberOfDoors;
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
            Type = DTOs.Enums.VehicleType.Sedan,
            NumberOfDoors = NumberOfDoors
        };
    }
    
    public int? GetNumberOfDoors() => NumberOfDoors;
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfDoors < 2)
            throw new CustomExceptions.ValidationException("Sedan number of doors must be greater than 2.");
        
        if (NumberOfDoors > 5)
            throw new CustomExceptions.ValidationException("Sedan number of doors can't be greater than 5.");
    }
}