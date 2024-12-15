using CarAuctionManagement.DTOs.Bidder.Responses;

namespace CarAuctionManagement.Handlers.Bidders.GetBidders;

public interface IGetBiddersHandler
{
    GetBiddersResponseDto? GetBidders(int page, int pageSize); 
    BidderResponseDto? GetBidderById(Guid? bidderId);
}