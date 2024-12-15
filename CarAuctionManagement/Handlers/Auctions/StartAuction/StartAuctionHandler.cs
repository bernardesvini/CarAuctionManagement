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
    
    public StartAuctionResponseDto? StartAuction(StartAuctionRequestDto auctionRequest)
    {
        var validator = new StartAuctionRequestDto.StartAuctionRequestDtoValidator();
        var validationResult = validator.Validate(auctionRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Vehicle? vehicle = _vehiclesService.GetVehicleById(auctionRequest.VehicleId);
        
        var auction = new Auction
        (
            Guid.NewGuid(),
            vehicle,
            true,
            new List<Bid?>()
        );
        Auction? addedAuction = _auctionsService.StartAuction(auction);

        StartAuctionResponseDto? response = addedAuction?.ToResponseDto();
        
        return response;
    }
}