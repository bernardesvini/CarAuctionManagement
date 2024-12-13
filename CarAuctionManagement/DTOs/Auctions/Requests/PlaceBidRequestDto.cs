using FluentValidation;

namespace CarAuctionManagement.DTOs.Auctions.Requests;

public class PlaceBidRequestDto
{
    public string? BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public decimal? Amount { get; set; }
    
    public class PlaceBidRequestDtoValidator : AbstractValidator<PlaceBidRequestDto>
    {
        public PlaceBidRequestDtoValidator()
        {
            RuleFor(x => x.BidderId).NotEmpty().WithMessage("BidderId must be provided.");
            RuleFor(x => x.AuctionId).NotEmpty().WithMessage("AuctionId must be provided.");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount must be provided.");
        }
    }
}