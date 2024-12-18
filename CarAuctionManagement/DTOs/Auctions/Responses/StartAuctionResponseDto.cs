﻿using CarAuctionManagement.DTOs.Vehicles.Responses;

namespace CarAuctionManagement.DTOs.Auctions.Responses;

public class StartAuctionResponseDto
{
    public Guid? Id { get; set; }
    public VehicleResponseDto? Vehicle { get; set; }
    public decimal? HighestBid { get; set; }
}