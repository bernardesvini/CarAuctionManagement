using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Auctions;

public class Bid
{
    public Guid Id { get; set; }
    public string? BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public decimal? Amount { get; set; }
    
    public void Validate()
    {
        if (Guid.Empty.Equals(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (string.IsNullOrEmpty(BidderId))
        {
            throw new CustomExceptions.ValidationException("BidderId must be provided.");
        }

        if (Guid.Empty.Equals(AuctionId))
        {
            throw new CustomExceptions.ValidationException("AuctionId must be provided.");
        }

        if (Amount <= 0)
        {
            throw new CustomExceptions.ValidationException("Amount must be greater than 0.");
        }
    }
}
