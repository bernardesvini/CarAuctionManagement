using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Auctions.Responses;


namespace CarAuctionManagement.Handlers.Auctions.StartAuction;

public interface IStartAuctionHandler
{
    StartAuctionResponseDto? StartAuction(StartAuctionRequestDto auctionRequest);
}