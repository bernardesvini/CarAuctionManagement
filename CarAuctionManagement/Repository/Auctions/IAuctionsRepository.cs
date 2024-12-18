﻿using CarAuctionManagement.Models.Auctions;

namespace CarAuctionManagement.Repository.Auctions;

public interface IAuctionsRepository
{
    Auction? StartAuction(Auction? auction);
    List<Auction?>? GetAuctions();
    Auction? GetAuctionById(Guid? id);
    void EndAuction(Guid? auctionId);
    Bid? PlaceBid(Bid? bidAdd);
    List<Auction?>? GetClosedAuctions();
    List<Auction?>? GetActiveAuctions();
    Auction? GetHighestBidderId(Guid? auctionId);
}