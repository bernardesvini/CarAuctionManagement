using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleRemoveRequestDto
{
    public Guid? Id { get; set; }
    
    public class VehicleRemoveRequestDtoValidator : AbstractValidator<VehicleRemoveRequestDto>
    {
        public VehicleRemoveRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Vehicle ID must be provided.");
        }
    }
}