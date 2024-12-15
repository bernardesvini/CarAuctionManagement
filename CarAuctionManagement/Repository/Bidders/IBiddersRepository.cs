using CarAuctionManagement.Models.Bidders;

namespace CarAuctionManagement.Repository.Bidders;

public interface IBiddersRepository
{
    Bidder? AddBidder(Bidder? bidder);
    List<Bidder?>? GetBidders();
    List<Bidder?>? GetActivesBidders();
    List<Bidder?>? GetInactivesBidders();
    
    Bidder? GetBidderById(Guid? bidderId);
    Bidder? UpdateBidder(Bidder? bidder);
    void RemoveBidder(Guid? bidderId);
}