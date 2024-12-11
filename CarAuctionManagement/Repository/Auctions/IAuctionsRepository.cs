using CarAuctionManagement.Models;
using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Repository.Auctions;

public interface IAuctionsRepository
{
    void StartAuction(Auction auction);
    List<Auction> GetAuctions();
    void EndAuction(Auction auction);
    void PlaceBid(Bid? bidAdd);
    List<Auction>? GetClosedAuctions();
    List<Auction>? GetActiveAuctions();
}