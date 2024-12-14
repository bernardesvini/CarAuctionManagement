using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Handlers.Auctions.GetAuctions;

public interface IGetAuctionHandler
{
    List<Auction?>? GetAuctions();
    List<Auction?>? GetActiveAuctions();
    List<Auction?>? GetClosedAuctions();
}