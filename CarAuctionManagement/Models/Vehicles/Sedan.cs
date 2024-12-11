using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Sedan : Vehicle
{
    public int NumberOfDoors { get; set; }
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfDoors < 3)
            throw new CustomExceptions.ValidationException("Sedan number of doors must be greater than 3.");
        
        if (NumberOfDoors > 5)
            throw new CustomExceptions.ValidationException("Sedan number of doors can't be greater than 5.");
    }
}