using CarAuctionManagement.DTOs.Enums;
using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleSearchRequestDto
{
    public string? Type { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public Guid? Id { get; set; }

    public class VehicleSearchRequestDtoValidator : AbstractValidator<VehicleSearchRequestDto>
    {
        public VehicleSearchRequestDtoValidator()
        {
            RuleFor(x => x.StartYear)
                .Must(year => year == null || year == 0 || (year >= 1886 && year <= DateTime.Now.Year))
                .WithMessage("Start year must be valid.");
            
            RuleFor(x => x.EndYear)
                .Must(year => year == null || year == 0 || (year >= 1886 && year <= DateTime.Now.Year))
                .WithMessage("End year must be valid.");
            
            RuleFor(x => x.StartYear)
                .Must((endYear, startYear) => startYear == null || endYear.EndYear == null || startYear <= endYear.EndYear)
                .WithMessage("Start year must be less than or equal to end year.");
            
            RuleFor(x => x.EndYear)
                .Must((endYear, startYear) => startYear == null || endYear.EndYear == null || endYear.EndYear >= startYear)
                .WithMessage("End year must be greater than or equal to start year.");

            RuleFor(x => x.Type)
                .Must(type => string.IsNullOrEmpty(type) || Enum.TryParse(typeof(VehicleType), type, out _))
                .WithMessage("Vehicle type must be valid.");
        }
    }
}