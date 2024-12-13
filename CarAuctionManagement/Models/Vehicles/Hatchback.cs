using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Hatchback : Vehicle
{
    public int? NumberOfDoors { get; set; }
    
    public override void Validate()
    {
        base.Validate();
        
        if (NumberOfDoors < 2)
            throw new CustomExceptions.ValidationException("Hatchback number of doors must be greater than 2.");
        if (NumberOfDoors > 5)
            throw new CustomExceptions.ValidationException("Hatchback number of doors can't be greater than 5.");
        
    }
}