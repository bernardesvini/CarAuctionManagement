using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Models.Auctions;

public class Auction
{
    private Guid? Id { get; }
    private Vehicle? Vehicle { get; }
    private bool IsActive { get; set; }
    private List<Bid?>? Bids { get; }
    private decimal? HighestBid { get; set; }
    private Guid? HighestBidder { get; set; }

    public Auction(Guid? id, Vehicle? vehicle, bool isActive, List<Bid?>? bids)
    {
        Id = id;
        Vehicle = vehicle;
        IsActive = isActive;
        Bids = bids;
        HighestBid = 0;
        HighestBidder = null;
        Validate();
    }
    
    public Auction(Guid? id, Vehicle? vehicle, bool isActive, List<Bid?>? bids, decimal? highestBid, Guid? highestBidder)
    {
        Id = id;
        Vehicle = vehicle;
        IsActive = isActive;
        Bids = bids;
        HighestBid = highestBid;
        HighestBidder = highestBidder;
        Validate();
    }
    
    public Guid? GetId() => Id;
    public Vehicle? GetVehicle() => Vehicle;
    public bool GetIsActive() => IsActive;
    public List<Bid?>? GetBids() => Bids;
    public decimal? GetHighestBid() => HighestBid;
    public Guid? GetHighestBidder() => HighestBidder;
    public void SetIsActive(bool isActive) => IsActive = isActive;
    public void SetHighestBid(decimal? highestBid) => HighestBid = highestBid;
    public void SetHighestBidder(Guid? highestBidder) => HighestBidder = highestBidder;
    
    
    public StartAuctionResponseDto ToResponseDto()
    {
        return new StartAuctionResponseDto
        {
            Id = GetId(),
            Vehicle = Vehicle?.ToResponseDto(),
            HighestBid = GetHighestBid(),
        };
    }
    
    public AuctionResponseDto ToGetResponseDto()
    {
        return new AuctionResponseDto
        {
            Id = GetId(),
            Vehicle = Vehicle?.ToResponseDto(),
            IsActive = GetIsActive(),
            Bids = GetBids()?.Select(b => b?.ToResponseDto()).OrderBy(bid => bid?.Amount).ToList(),
            HighestBid = GetHighestBid(),
            HighestBidder = GetHighestBidder()
        };
    }

    private void Validate()
    {
        Vehicle?.Validate();
        
        if (Guid.Empty.Equals(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (Vehicle == null)
        {
            throw new CustomExceptions.ValidationException("Vehicle must be provided.");
        }

    }
}