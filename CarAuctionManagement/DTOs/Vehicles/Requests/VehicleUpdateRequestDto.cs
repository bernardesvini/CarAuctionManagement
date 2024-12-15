using CarAuctionManagement.DTOs.Enums;
using FluentValidation;

namespace CarAuctionManagement.DTOs.Vehicles.Requests;

public class VehicleUpdateRequestDto
{
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? StartingBid { get; set; }
    public VehicleType? Type { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public decimal? LoadCapacity { get; set; }
    
    public class VehicleUpdateRequestDtoValidator : AbstractValidator<VehicleUpdateRequestDto>
    {
        public VehicleUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Type).IsInEnum().WithMessage("Vehicle type must be valid.");
            When(x => x.Type == VehicleType.Sedan || x.Type == VehicleType.Hatchback , () =>
            {

                RuleFor(x => x.LoadCapacity)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Load capacity is not applicable for Hatchback and Sedan.");

                RuleFor(x => x.NumberOfSeats)
                    .Must(value => value == null || value == 0)
                    .WithMessage("Number of seats is not applicable for Hatchback and Sedan.");
            });
            

            When(x => x.Type == VehicleType.Suv, () =>
            {

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