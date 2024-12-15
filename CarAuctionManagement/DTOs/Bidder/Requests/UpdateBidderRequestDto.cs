using FluentValidation;

namespace CarAuctionManagement.DTOs.Bidder.Requests;

public class UpdateBidderRequestDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    public class UpdateBidderRequestDtoValidator : AbstractValidator<UpdateBidderRequestDto>
    {
        public UpdateBidderRequestDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("A valid email must be provided.");
        }
    }
}