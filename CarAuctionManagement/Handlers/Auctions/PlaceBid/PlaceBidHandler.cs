using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Bidders;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Auctions.PlaceBid;

public class PlaceBidHandler : IPlaceBidHandler
{
    private readonly IAuctionsService _auctionsService;
    private readonly IBiddersService _biddersService;

    public PlaceBidHandler(IAuctionsService auctionsService, IBiddersService biddersService)
    {
        _auctionsService = auctionsService;
        _biddersService = biddersService;
    }

    public PlaceBidResponseDto? PlaceBid(PlaceBidRequestDto bid)
    {
        var validator = new PlaceBidRequestDto.PlaceBidRequestDtoValidator();
        var validationResult = validator.Validate(bid);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        _biddersService.GetBidderById(bid.BidderId);

        Bid newBid = new Bid
        (
            Guid.NewGuid(),
            bid.BidderId,
            bid.AuctionId,
            bid.Amount
        );

        Bid? addedBid = _auctionsService.PlaceBid(newBid);

        PlaceBidResponseDto? response = addedBid?.ToResponseDto();

        return response;
    }
}