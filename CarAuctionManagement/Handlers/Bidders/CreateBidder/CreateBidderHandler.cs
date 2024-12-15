using CarAuctionManagement.DTOs.Bidder.Requests;
using CarAuctionManagement.DTOs.Bidder.Responses;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Services.Bidders;
using FluentValidation;

namespace CarAuctionManagement.Handlers.Bidders.CreateBidder;

public class CreateBidderHandler : ICreateBidderHandler
{
    private readonly IBiddersService _biddersService;
    
    public CreateBidderHandler(IBiddersService biddersService)
    {
        _biddersService = biddersService;
    }
    
    public BidderResponseDto? CreateBidder(CreateBidderRequestDto bidder)
    {
        var validator = new CreateBidderRequestDto.CreateBidderRequestDtoValidator();
        var validationResult = validator.Validate(bidder);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Bidder newBidder = new Bidder
        (
            Guid.NewGuid(),
            bidder.Name,
            bidder.Email
        );

        Bidder? addedBidder = _biddersService.CreateBidder(newBidder);

        BidderResponseDto? response = addedBidder?.ToResponseDto();

        return response;
    }
    
    public BidderResponseDto? UpdateBidder(UpdateBidderRequestDto bidder, Guid id)
    {
        var validator = new UpdateBidderRequestDto.UpdateBidderRequestDtoValidator();
        var validationResult = validator.Validate(bidder);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Bidder? existingBidder = _biddersService.GetBidderById(id);
        Bidder updatedBidder = new Bidder
        (
            id,
            string.IsNullOrEmpty(bidder.Name)? existingBidder?.GetName() : bidder.Name,
            string.IsNullOrEmpty(bidder.Email)? existingBidder?.GetEmail() : bidder.Email
        );

        Bidder? updatedBidderResponse = _biddersService.UpdateBidder(updatedBidder);

        BidderResponseDto? response = updatedBidderResponse?.ToResponseDto();

        return response;
    }
    
}