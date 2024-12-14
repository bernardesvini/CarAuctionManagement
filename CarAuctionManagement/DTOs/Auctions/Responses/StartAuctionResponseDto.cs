using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.DTOs.Auctions.Responses;

public class StartAuctionResponseDto
{
    public Guid? Id { get; set; }
    public Vehicle? Vehicle { get; set; }
    public decimal? HighestBid { get; set; }
}