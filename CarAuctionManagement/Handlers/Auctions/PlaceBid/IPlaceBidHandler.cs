using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Auctions.Responses;

namespace CarAuctionManagement.Handlers.Auctions.PlaceBid;

public interface IPlaceBidHandler
{
    PlaceBidResponseDto PlaceBid(PlaceBidRequestDto bid);
}