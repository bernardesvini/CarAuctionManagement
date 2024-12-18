﻿namespace CarAuctionManagement.DTOs.Auctions.Responses;

public class PlaceBidResponseDto
{
    public Guid? Id { get; set; }
    public Guid? BidderId { get; set; }
    public Guid? AuctionId { get; set; }
    public decimal? Amount { get; set; }
}