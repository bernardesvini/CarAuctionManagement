using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Models.Auctions;

public class Auction
{
    public string? Id { get; set; }
    public Vehicle? Vehicle { get; set; }
    public bool IsActive { get; set; }
    public List<Bid?>? Bids { get; set; }
    public double HighestBid { get; set; }
    public string? HighestBidder { get; set; }
    
    public void Validate()
    {
        Vehicle?.Validate();
        
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (Vehicle == null)
        {
            throw new CustomExceptions.ValidationException("Vehicle must be provided.");
        }

    }
}