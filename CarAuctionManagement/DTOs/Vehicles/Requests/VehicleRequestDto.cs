using CarAuctionManagement.DTOs.Enums;
using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleRequestDto
{
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? StartingBid { get; set; }
    public VehicleType? Type { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public decimal? LoadCapacity { get; set; }
    
    public class VehicleRequestDtoValidator : AbstractValidator<VehicleRequestDto>
    {
        public VehicleRequestDtoValidator()
        {
            RuleFor(x => x.Manufacturer).NotEmpty().NotNull().WithMessage("Manufacturer must be provided.");
            RuleFor(x => x.Model).NotEmpty().NotNull().WithMessage("Model must be provided.");
            RuleFor(x => x.StartingBid).GreaterThan(0).WithMessage("Starting bid must be greater than 0.");
            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year)
                .WithMessage("Year must be a valid.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Vehicle type must be valid.");
            
            
            When(x => x.Type == VehicleType.Sedan || x.Type == VehicleType.Hatchback , () =>
            {
                RuleFor(x => x.NumberOfDoors)
                    .InclusiveBetween(2, 5)
                    .WithMessage("Number of doors must be between 2 and 5 for Hatchback and Sedan.");

                RuleFor(x => x.LoadCapacity)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Load capacity is not applicable for Hatchback and Sedan.");

                RuleFor(x => x.NumberOfSeats)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Number of seats is not applicable for Hatchback and Sedan.");
            });
            

            When(x => x.Type == VehicleType.Suv, () =>
            {
                RuleFor(x => x.NumberOfSeats)
                    .InclusiveBetween(1, 10)
                    .WithMessage("Number of seats must be between 1 and 10 for SUV.");

                RuleFor(x => x.LoadCapacity)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Load capacity is not applicable for SUVs.");

                RuleFor(x => x.NumberOfDoors)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Number of doors is not applicable for SUVs.");
            });
            
            When(x => x.Type == VehicleType.Truck, () =>
            {
                RuleFor(x => x.LoadCapacity)
                    .GreaterThan(0)
                    .WithMessage("Load capacity must be greater than 0 for Trucks.");
            
                RuleFor(x => x.LoadCapacity)
                    .LessThan(1000000)
                    .WithMessage("Load capacity exceeds the maximum.");

                RuleFor(x => x.NumberOfDoors)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Number of doors is not applicable for Trucks.");

                RuleFor(x => x.NumberOfSeats)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Number of seats is not applicable for Trucks.");
            });

        }
    }
}