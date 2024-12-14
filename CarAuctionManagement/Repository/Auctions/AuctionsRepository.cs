﻿
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
    
    public Auction? StartAuction(Auction? auction)
    {
        _database.Auctions?.Add(auction);
        return auction;
    }
    
    public List<Auction?>? GetAuctions()
    {
        return _database.Auctions;
    }

    public void EndAuction(Guid? auctionId)
    {
        _database.Auctions?.Where(activeAuction => activeAuction?.GetId() == auctionId).ToList().ForEach(auction1 =>
        {
            if (auction1 != null) auction1.SetIsActive(false);
        });
    }
    
    public Bid? PlaceBid(Bid? bidAdd)
    {
        _database.Auctions?.Where(activeAuction => activeAuction?.GetId() == bidAdd?.GetAuctionId()).ToList().ForEach(auction1 =>
        {
            if (auction1 != null) auction1.SetHighestBid(bidAdd?.GetAmount());
            if (auction1 != null) auction1.SetHighestBidder(bidAdd?.GetBidderId());
            auction1?.GetBids()?.Add(bidAdd);
        });
        return bidAdd;
    }

    public List<Auction?>? GetClosedAuctions()
    {
        return _database.Auctions?.Where(auction => auction?.GetIsActive() == false).ToList();
    }

    public List<Auction?>? GetActiveAuctions()
    {
        return _database.Auctions?.Where(auction => auction != null && auction.GetIsActive()).ToList();
    }
}