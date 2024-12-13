using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Services.Auctions;

public interface IAuctionsService
{
    Auction? StartAuction(Auction? auction);
    List<Auction?>? GetAuctions();
    void EndAuction(Guid auctionId);
    Bid? PlaceBid(Bid? newBid);
    List<Auction?>? GetActiveAuctions();
    List<Auction?>? GetClosedAuctions();
}