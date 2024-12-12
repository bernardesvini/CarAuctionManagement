using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Vehicle
{
    public string? Id { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? StartingBid { get; set; }

    public virtual void Validate()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new CustomExceptions.ValidationException("Vehicle id must be provided.");
        }

        if (string.IsNullOrWhiteSpace(Manufacturer))
        {
            throw new CustomExceptions.ValidationException("Manufacturer must be provided.");
        }

        if (string.IsNullOrWhiteSpace(Model))
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