using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Auctions;

public class Bid
{
    public string? Id { get; set; }
    public string? BidderId { get; set; }
    public string? AuctionId { get; set; }
    public decimal? Amount { get; set; }
    
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (string.IsNullOrWhiteSpace(BidderId))
        {
            throw new CustomExceptions.ValidationException("BidderId must be provided.");
        }

        if (string.IsNullOrWhiteSpace(AuctionId))
        {
            throw new CustomExceptions.ValidationException("AuctionId must be provided.");
        }

        if (Amount <= 0)
        {
            throw new CustomExceptions.ValidationException("Amount must be greater than 0.");
        }
    }
}
