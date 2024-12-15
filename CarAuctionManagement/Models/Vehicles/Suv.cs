using CarAuctionManagement.DTOs.Vehicles.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public sealed class Suv : Vehicle
{
    private int? NumberOfSeats { get; set; }
    
    public Suv(Guid? id, string? manufacturer, string? model, int? year, decimal? startingBid, int? numberOfSeats)
        : base(id, manufacturer, model, year, startingBid)
    {
        NumberOfSeats = numberOfSeats;
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
            Type = DTOs.Enums.VehicleType.Suv,
            NumberOfSeats = NumberOfSeats
        };
    }
    
    public int? GetNumberOfSeats() => NumberOfSeats;
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfSeats == 0 || NumberOfSeats > 10)
            throw new CustomExceptions.ValidationException("Number of seats must be between 1 and 10 for SUV.");
    }
}