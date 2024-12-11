
using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Repository.Auctions;

public class AuctionsRepository : IAuctionsRepository
{
    private List<Auction> _auctions = new();
    
    public void StartAuction(Auction auction)
    {
        _auctions.Add(auction);
    }
    
    public List<Auction> GetAuctions()
    {
        return _auctions;
    }

    public void EndAuction(Auction auction)
    {
        _auctions.Where(activeAuction => activeAuction == auction).ToList().ForEach(auction1 => auction1.IsActive = false);
    }
    
    public void PlaceBid(Bid? bidAdd)
    {
        _auctions.Where(activeAuction => activeAuction.Id == bidAdd?.AuctionId).ToList().ForEach(auction1 =>
        {
            auction1.HighestBid = bidAdd.Amount;
            auction1.HighestBidder = bidAdd?.BidderId;
            auction1?.Bids?.Add(bidAdd);
        });
    }

    public List<Auction>? GetClosedAuctions()
    {
        return _auctions.Where(auction => auction.IsActive == false).ToList();
    }

    public List<Auction>? GetActiveAuctions()
    {
        return _auctions.Where(auction => auction.IsActive).ToList();
    }
}