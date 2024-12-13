using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Suv : Vehicle
{
    public int? NumberOfSeats { get; set; }
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfSeats < 4)
            throw new CustomExceptions.ValidationException("SUV number of seats must be greater than 4.");
    }
}