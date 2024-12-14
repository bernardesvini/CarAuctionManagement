using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Auctions.StartAuction;

public class StartAuctionHandler : IStartAuctionHandler
{
    private readonly IAuctionsService _auctionsService;
    private readonly IVehiclesService _vehiclesService;
    
    public StartAuctionHandler(IAuctionsService auctionsService, IVehiclesService vehiclesService)
    {
        _auctionsService = auctionsService;
        _vehiclesService = vehiclesService;
    }
    
    public StartAuctionResponseDto StartAuction(StartAuctionRequestDto auctionRequest)
    {
        var validator = new StartAuctionRequestDto.StartAuctionRequestDtoValidator();
        var validationResult = validator.Validate(auctionRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Vehicle? vehicle = _vehiclesService.GetVehicleById(auctionRequest.VehicleId);
        
        var auction = new Auction
        {
            Id = Guid.NewGuid(),
            Vehicle = vehicle,
            Bids = new List<Bid?>(),
            IsActive = true,
            HighestBid = vehicle?.StartingBid,
            HighestBidder = string.Empty
        };
        Auction? addedAuction = _auctionsService.StartAuction(auction);

        StartAuctionResponseDto response = new StartAuctionResponseDto
        {
            Id = addedAuction?.Id,
            Vehicle = addedAuction?.Vehicle,
            HighestBid = addedAuction?.HighestBid
        };
        
        return response;
    }
}