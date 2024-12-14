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
            Type = DTOs.Enums.VehicleType.Hatchback,
            NumberOfDoors = 0,
            NumberOfSeats = NumberOfSeats,
            LoadCapacity = 0
        };
    }
    
    public int? GetNumberOfSeats() => NumberOfSeats;
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfSeats < 4)
            throw new CustomExceptions.ValidationException("SUV number of seats must be greater than 4.");
    }
}