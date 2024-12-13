using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.Services.Auctions;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Auctions.EndAuction;

public class EndAuctionHandler : IEndAuctionHandler
{
    private readonly IAuctionsService _auctionsService;
    
    public EndAuctionHandler(IAuctionsService auctionsService)
    {
        _auctionsService = auctionsService;
    }
    
    public void EndAuction(EndAuctionRequestDto auction)
    {
        var validator = new EndAuctionRequestDto.EndAuctionRequestDtoValidator();
        var validationResult = validator.Validate(auction);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        _auctionsService.EndAuction(auction.AuctionId);
    }
}