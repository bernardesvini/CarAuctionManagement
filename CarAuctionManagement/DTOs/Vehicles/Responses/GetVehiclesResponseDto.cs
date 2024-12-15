namespace CarAuctionManagement.DTOs.Vehicles.Responses;

public class GetVehiclesResponseDto
{
    public int? TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<VehicleResponseDto?>? VehiclesList { get; set; }
}