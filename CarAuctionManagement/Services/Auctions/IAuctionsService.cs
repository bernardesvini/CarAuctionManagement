using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Bidders;

namespace CarAuctionManagement.Services.Auctions;

public interface IAuctionsService
{
    Auction? StartAuction(Auction? auction);
    List<Auction?>? GetAuctions();
    Auction? GetAuctionById(Guid? id);
    void EndAuction(Guid? auctionId);
    Bid? PlaceBid(Bid? newBid);
    List<Auction?>? GetActiveAuctions();
    List<Auction?>? GetClosedAuctions();
    Bidder? GetHighestBidder(Guid? auctionId);
}