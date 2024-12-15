using CarAuctionManagement.Models.Bidders;

namespace CarAuctionManagement.Repository.Bidders;

public interface IBiddersRepository
{
    Bidder? AddBidder(Bidder? bidder);
    List<Bidder?>? GetBidders();
    Bidder? UpdateBidder(Bidder? bidder);
    void RemoveBidder(Guid? bidderId);
}