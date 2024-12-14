using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Services.Auctions;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Auctions.PlaceBid;

public class PlaceBidHandler : IPlaceBidHandler
{
    private readonly IAuctionsService _auctionsService;
    
    public PlaceBidHandler(IAuctionsService auctionsService)
    {
        _auctionsService = auctionsService;
    }
    
    public PlaceBidResponseDto PlaceBid(PlaceBidRequestDto bid)
    {
        
        
        var validator = new PlaceBidRequestDto.PlaceBidRequestDtoValidator();
        var validationResult = validator.Validate(bid);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Bid newBid = new Bid
        {
            Id = Guid.NewGuid(),
            AuctionId = bid.AuctionId,
            Amount = bid.Amount,
            BidderId = bid.BidderId
        };
        
        Bid? addedBid = _auctionsService.PlaceBid(newBid);

        PlaceBidResponseDto response = new PlaceBidResponseDto
        {
            Id = addedBid?.Id,
            AuctionId = addedBid?.AuctionId,
            Amount = addedBid?.Amount,
            BidderId = addedBid?.BidderId
        };
        
        return response;
    }
}