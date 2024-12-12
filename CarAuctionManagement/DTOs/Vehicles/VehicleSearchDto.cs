namespace CarAuctionManagement.DTOs.Vehicles;

public class VehicleSearchDto
{
    public string? Type { get; set; } = null;
    public string? Manufacturer { get; set; } = null;
    public string? Model { get; set; } = null;
    public int? Year { get; set; } = null;
}