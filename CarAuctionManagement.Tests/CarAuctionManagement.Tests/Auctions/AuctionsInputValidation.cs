using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Auctions;

public class AuctionsInputValidation
{
    [Fact]
    public void Constructor_ShouldInitializeAuctionProperties()
    {
        var id = Guid.NewGuid();
        var vehicle = new Sedan(id, "Honda", "Civic", 2019, 1500.00m, 4);
        var bids = new List<Bid?>();

        var auction = new Auction(id, vehicle, true, bids);

        Assert.Equal(id, auction.GetId());
        Assert.Equal(vehicle, auction.GetVehicle());
        Assert.True(auction.GetIsActive());
        Assert.Equal(bids, auction.GetBids());
        Assert.Equal(0, auction.GetHighestBid());
        Assert.Null(auction.GetHighestBidder());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenIdAuctionIsEmpty()
    {
        var vehicle = new Sedan(Guid.NewGuid(), "Honda", "Civic", 2019, 1500.00m, 4);
        var bids = new List<Bid?>();

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Auction(Guid.Empty, vehicle, true, bids));
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenVehicleIsNull()
    {
        var bids = new List<Bid?>();

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Auction(Guid.NewGuid(), null, true, bids));
        Assert.Equal("Vehicle must be provided.", exception.Message);
    }
    
    [Fact]
    public void Constructor_ShouldInitializeBidProperties()
    {
        var id = Guid.NewGuid();
        var bidderId = Guid.NewGuid();
        var auctionId = Guid.NewGuid();
        var amount = 1000.00m;

        var bid = new Bid(id, bidderId, auctionId, amount);

        Assert.Equal(id, bid.GetId());
        Assert.Equal(bidderId, bid.GetBidderId());
        Assert.Equal(auctionId, bid.GetAuctionId());
        Assert.Equal(amount, bid.GetAmount());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidIdIsEmpty()
    {
        var bidderId = Guid.NewGuid();
        var auctionId = Guid.NewGuid();
        var amount = 1000.00m;

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Bid(Guid.Empty, bidderId, auctionId, amount));
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidderIdIsEmpty()
    {
        var id = Guid.NewGuid();
        var auctionId = Guid.NewGuid();
        var amount = 1000.00m;

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Bid(id, Guid.Empty, auctionId, amount));
        Assert.Equal("BidderId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenAuctionIdIsEmpty()
    {
        var id = Guid.NewGuid();
        var bidderId = Guid.NewGuid();
        var amount = 1000.00m;

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Bid(id, bidderId, Guid.Empty, amount));
        Assert.Equal("AuctionId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenAmountIsLessThanOrEqualToZero()
    {
        var id = Guid.NewGuid();
        var bidderId = Guid.NewGuid();
        var auctionId = Guid.NewGuid();

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Bid(id, bidderId, auctionId, 0));
        Assert.Equal("Amount must be greater than 0.", exception.Message);

        exception = Assert.Throws<CustomExceptions.ValidationException>(() => new Bid(id, bidderId, auctionId, -1));
        Assert.Equal("Amount must be greater than 0.", exception.Message);
    }
}