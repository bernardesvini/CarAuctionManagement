using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.DTOs.Auctions.Responses;

public class GetAuctionResponseDto
{
    public Guid? Id { get; set; }
    public VehicleResponseDto? Vehicle { get; set; }
    public bool? IsActive { get; set; }
    public List<PlaceBidResponseDto?>? Bids { get; set; }
    public decimal? HighestBid { get; set; }
    public string? HighestBidder { get; set; }
}