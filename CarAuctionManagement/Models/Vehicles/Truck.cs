using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Vehicles;

public class Truck : Vehicle
{
    public double? LoadCapacity { get; set; }
    
    public override void Validate()
    {
        base.Validate();

        if (LoadCapacity <= 0)
            throw new CustomExceptions.ValidationException("Truck load capacity must be greater than 0.");

        if (LoadCapacity > 1000000) 
            throw new CustomExceptions.ValidationException("Truck load capacity exceeds maximum limit.");
    }
}