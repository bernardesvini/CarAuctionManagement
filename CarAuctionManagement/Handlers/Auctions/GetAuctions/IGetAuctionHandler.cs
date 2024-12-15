using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.DTOs.Bidder.Responses;

namespace CarAuctionManagement.Handlers.Auctions.GetAuctions;

public interface IGetAuctionHandler
{
    GetAuctionResponseDto? GetAuctions(int page, int pageSize);
    AuctionResponseDto? GetAuctionById(Guid? auctionId);
    GetAuctionResponseDto? GetActiveAuctions(int page, int pageSize);
    GetAuctionResponseDto? GetClosedAuctions(int page, int pageSize);
    BidderResponseDto? GetHighestBidder(Guid? auctionId);
}