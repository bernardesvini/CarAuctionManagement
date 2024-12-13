using CarAuctionManagement.DTOs.Auctions.Requests;

namespace CarAuctionManagement.Handlers.Auctions.EndAuction;

public interface IEndAuctionHandler
{
    void EndAuction(EndAuctionRequestDto auction);
}