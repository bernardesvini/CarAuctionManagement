using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Services.Auctions;

public interface IAuctionsService
{
    void StartAuction(Auction auction);
    List<Auction> GetAuctions();
    void EndAuction(Auction auction);
    void PlaceBid(Bid newBid);
    List<Auction>? GetActiveAuctions();
    List<Auction>? GetClosedAuctions();
}