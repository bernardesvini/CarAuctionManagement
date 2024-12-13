using CarAuctionManagement.DTOs.Enums;
using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleSearchRequestDto
{
    public string? Type { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }

    public class VehicleSearchRequestDtoValidator : AbstractValidator<VehicleSearchRequestDto>
    {
        public VehicleSearchRequestDtoValidator()
        {
            RuleFor(x => x.Year)
                .Must(year => year == null || year == 0 || (year >= 1886 && year <= DateTime.Now.Year))
                .WithMessage("Year must be valid.");
            RuleFor(x => x.Type).Must(type => string.IsNullOrEmpty(type) || Enum.TryParse(typeof(VehicleType), type, out _)).WithMessage("Vehicle type must be valid.");
        }
    }
}