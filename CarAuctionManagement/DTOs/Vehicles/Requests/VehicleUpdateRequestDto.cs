using CarAuctionManagement.DTOs.Enums;
using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleUpdateRequestDto
{
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? StartingBid { get; set; }
    public VehicleType? Type { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public decimal? LoadCapacity { get; set; }
}