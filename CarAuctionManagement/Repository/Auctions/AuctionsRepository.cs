
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
    
    public void StartAuction(Auction auction)
    {
        _database.Auctions.Add(auction);
    }
    
    public List<Auction> GetAuctions()
    {
        return _database.Auctions;
    }

    public void EndAuction(Auction auction)
    {
        _database.Auctions.Where(activeAuction => activeAuction == auction).ToList().ForEach(auction1 => auction1.IsActive = false);
    }
    
    public void PlaceBid(Bid? bidAdd)
    {
        _database.Auctions.Where(activeAuction => activeAuction.Id == bidAdd?.AuctionId).ToList().ForEach(auction1 =>
        {
            auction1.HighestBid = bidAdd?.Amount;
            auction1.HighestBidder = bidAdd?.BidderId;
            auction1.Bids?.Add(bidAdd);
        });
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