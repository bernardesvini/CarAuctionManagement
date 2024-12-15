using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Auctions;

public class Bid
{
    private Guid? Id { get; set; }
    private Guid? BidderId { get; set; }
    private Guid? AuctionId { get; set; }
    private decimal? Amount { get; set; }
    
    public Bid(Guid? id, Guid? bidderId, Guid? auctionId, decimal? amount)
    {
        Id = id;
        BidderId = bidderId;
        AuctionId = auctionId;
        Amount = amount;
        Validate();
    }
    
    public Guid? GetId() => Id;
    public Guid? GetBidderId() => BidderId;  
    public Guid? GetAuctionId() => AuctionId;
    public decimal? GetAmount() => Amount;
    
    public PlaceBidResponseDto ToResponseDto()
    {
        return new PlaceBidResponseDto
        {
            Id = GetId(),
            BidderId = GetBidderId(),
            AuctionId = GetAuctionId(),
            Amount = GetAmount()
        };
    }
    
    public void Validate()
    {
        if (Guid.Empty.Equals(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (Guid.Empty.Equals(BidderId))
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
