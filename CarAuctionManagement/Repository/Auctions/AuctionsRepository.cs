
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Repository.Database;

namespace CarAuctionManagement.Repository.Auctions;

public class AuctionsRepository : IAuctionsRepository
{
    private readonly InMemoryDatabase _database;
    
    public AuctionsRepository(InMemoryDatabase database)
    {
        _database = database;
    }
    
    public Auction StartAuction(Auction auction)
    {
        _database.Auctions.Add(auction);
        return auction;
    }
    
    public List<Auction> GetAuctions()
    {
        return _database.Auctions;
    }

    public void EndAuction(Guid auctionId)
    {
        _database.Auctions.Where(activeAuction => activeAuction.Id == auctionId).ToList().ForEach(auction1 => auction1.IsActive = false);
    }
    
    public Bid? PlaceBid(Bid? bidAdd)
    {
        _database.Auctions.Where(activeAuction => activeAuction.Id == bidAdd?.AuctionId).ToList().ForEach(auction1 =>
        {
            auction1.HighestBid = bidAdd?.Amount;
            auction1.HighestBidder = bidAdd?.BidderId;
            auction1.Bids?.Add(bidAdd);
        });
        return bidAdd;
    }

    public List<Auction> GetClosedAuctions()
    {
        return _database.Auctions.Where(auction => auction.IsActive == false).ToList();
    }

    public List<Auction> GetActiveAuctions()
    {
        return _database.Auctions.Where(auction => auction.IsActive).ToList();
    }
}