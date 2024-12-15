namespace CarAuctionManagement.Handlers.Bidders.RemoveBidders;

public interface IRemoveBidderHandler
{
    void RemoveBidder(Guid? bidderId);
}