using FluentValidation;

namespace CarAuctionManagement.DTOs.Bidder.Requests;

public class CreateBidderRequestDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    public class CreateBidderRequestDtoValidator : AbstractValidator<CreateBidderRequestDto>
    {
        public CreateBidderRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Bidder name must be provided.");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("A valid email must be provided.");
        }
    }
}