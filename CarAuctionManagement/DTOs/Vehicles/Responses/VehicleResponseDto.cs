using CarAuctionManagement.DTOs.Enums;

namespace CarAuctionManagement.DTOs.Vehicles.Responses;

public class VehicleResponseDto
{
    public Guid? Id { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? StartingBid { get; set; }
    public VehicleType? Type { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public decimal? LoadCapacity { get; set; }
}