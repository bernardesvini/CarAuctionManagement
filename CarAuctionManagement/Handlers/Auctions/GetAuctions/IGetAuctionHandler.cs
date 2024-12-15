using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Handlers.Auctions.GetAuctions;

public interface IGetAuctionHandler
{
    List<GetAuctionResponseDto?>? GetAuctions();
    GetAuctionResponseDto? GetAuctionById(Guid? auctionId);
    List<GetAuctionResponseDto?>? GetActiveAuctions();
    List<GetAuctionResponseDto?>? GetClosedAuctions();
}