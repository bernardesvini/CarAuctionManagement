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
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfDoors { get; set; }
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfSeats { get; set; }
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    public decimal? LoadCapacity { get; set; }
}