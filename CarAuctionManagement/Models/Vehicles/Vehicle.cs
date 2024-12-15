using CarAuctionManagement.DTOs.Vehicles.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Vehicle
{
    private Guid? Id { get; }
    private string? Manufacturer { get; }
    private string? Model { get; }
    private int? Year { get; }
    private decimal? StartingBid { get; }
    private bool IsDeleted { get; set; }
    
    public Vehicle(Guid? id, string? manufacturer, string? model, int? year, decimal? startingBid)
    {
        Id = id;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingBid = startingBid;
        IsDeleted = false;
    }
    
    public virtual VehicleResponseDto ToResponseDto()
    {
        return new VehicleResponseDto
        {
            Id = GetId(),
            Manufacturer = GetManufacturer(),
            Model = GetModel(),
            Year = GetYear(),
            StartingBid = GetStartingBid(),
            Type = DTOs.Enums.VehicleType.Hatchback,
        };
    }
    
    public Guid? GetId() => Id;
    public string? GetManufacturer() => Manufacturer;
    public string? GetModel() => Model;
    public int? GetYear() => Year;
    public decimal? GetStartingBid() => StartingBid;
    public bool GetIsDeleted() => IsDeleted;
    public void SetIsDeleted(bool isDeleted) => IsDeleted = isDeleted;
    
    public virtual void Validate()
    {
        if (Id == null || Guid.Empty.Equals(Id))
        {
            throw new CustomExceptions.ValidationException("Vehicle id must be provided.");
        }

        if (string.IsNullOrEmpty(Manufacturer))
        {
            throw new CustomExceptions.ValidationException("Manufacturer must be provided.");
        }

        if (string.IsNullOrEmpty(Model))
        {
            throw new CustomExceptions.ValidationException("Model must be provided.");
        }
        
        if (StartingBid <= 0)
        {
            throw new CustomExceptions.ValidationException("Starting bid must be greater than 0.");
        }
        
        if (Year < 1886 || Year > DateTime.Now.Year)
        {
            throw new CustomExceptions.ValidationException("Year must be a valid.");
        }
    }
}