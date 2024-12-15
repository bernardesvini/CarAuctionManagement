using CarAuctionManagement.DTOs.Bidder.Responses;

namespace CarAuctionManagement.Handlers.Bidders.GetBidders;

public interface IGetBiddersHandler
{
    GetBiddersResponseDto? GetBidders();
    BidderResponseDto? GetBidderById(Guid? bidderId);
}