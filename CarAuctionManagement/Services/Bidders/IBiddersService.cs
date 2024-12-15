using CarAuctionManagement.Models.Bidders;

namespace CarAuctionManagement.Services.Bidders;

public interface IBiddersService
{
    Bidder? CreateBidder(Bidder? bidder);
    List<Bidder?>? GetBidders();
    Bidder? GetBidderById(Guid? id);
    Bidder? UpdateBidder(Bidder? bidder);
    void RemoveBidder(Guid? id);
}