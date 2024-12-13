using FluentValidation;

namespace CarAuctionManagement.DTOs.Auctions.Requests;

public class StartAuctionRequestDto
{
    public Guid? VehicleId { get; set; }
    
    public class StartAuctionRequestDtoValidator : AbstractValidator<StartAuctionRequestDto>
    {
        public StartAuctionRequestDtoValidator()
        {
            RuleFor(x => x.VehicleId).NotNull().NotEmpty().WithMessage("Vehicle ID must be provided to start a auction.");
        }
    }
}