using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Repository.Database;

namespace CarAuctionManagement.Repository.Bidders;

public class BiddersRepository : IBiddersRepository
{
    private readonly InMemoryDatabase _database;
    
    public BiddersRepository(InMemoryDatabase database)
    {
        _database = database;
    }
    
    public Bidder? AddBidder(Bidder? bidder)
    {
        _database.Bidders?.Add(bidder);
        return bidder;
    }
    
    public List<Bidder?>? GetBidders()
    {
        return _database.Bidders?.Where(bidder => bidder != null && !bidder.GetIsDeleted()).ToList();
    }

    public List<Bidder?>? GetActivesBidders()
    {
        return _database.Bidders?.Where(bidder => bidder != null && !bidder.GetIsDeleted()).ToList();
    }

    public List<Bidder?>? GetInactivesBidders()
    {
        return _database.Bidders?.Where(bidder => bidder != null && bidder.GetIsDeleted()).ToList();
    }

    public Bidder? GetBidderById(Guid? bidderId)
    {
        return _database.Bidders?.FirstOrDefault(bidder => bidder?.GetId() == bidderId) ?? null;
    }

    public Bidder? UpdateBidder(Bidder? bidder)
    {
        _database.Bidders?.Where(existingBidder => existingBidder?.GetId() == bidder?.GetId())
            .ToList()
            .ForEach(existingBidder =>
                {
                    if (existingBidder != null)
                    {
                        var index = _database.Bidders.IndexOf(existingBidder);
                        _database.Bidders[index] = bidder;
                    }
                }
            );
        return bidder;
    }
    
    public void RemoveBidder(Guid? bidderId)
    {
        _database.Bidders?.Where(bidder => bidder?.GetId() == bidderId).ToList().ForEach(bidder =>
        {
            bidder?.SetIsDeleted(true);
        });
    }
}