using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Auctions;

public class AuctionsInputValidation
{
    [Fact]
    public void Validate_ShouldThrowException_WhenIdIsNull()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = null;
        auction.Object.Vehicle = new Sedan { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => auction.Object.Validate());
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenIdIsEmpty()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "";
        auction.Object.Vehicle = new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => auction.Object.Validate());
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenVehicleIsNull()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.Vehicle = null;

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => auction.Object.Validate());
        Assert.Equal("Vehicle must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldCallVehicleValidate_WhenVehicleIsNotNull()
    {
        var vehicleMock = new Mock<Truck>();
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "123";
        auction.Object.Vehicle = vehicleMock.Object;

        auction.Object.Validate();

        vehicleMock.Verify(v => v.Validate(), Times.Once);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidIdIsNull()
    {
        var bid = new Bid { Id = null, BidderId = "55", AuctionId = "456", Amount = 50 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidIdIsEmpty()
    {
        var bid = new Bid { Id = "", BidderId = "2", AuctionId = "13", Amount = 1589 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("Id must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidBidderIdIsNull()
    {
        var bid = new Bid { Id = "1", BidderId = null, AuctionId = "456", Amount = 2000 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("BidderId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidBidderIdIsEmpty()
    {
        var bid = new Bid { Id = "1", BidderId = "", AuctionId = "456", Amount = 100 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("BidderId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidAuctionIdIsNull()
    {
        var bid = new Bid { Id = "1", BidderId = "25", AuctionId = null, Amount = 1000 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("AuctionId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidAuctionIdIsEmpty()
    {
        var bid = new Bid { Id = "1", BidderId = "88", AuctionId = "", Amount = 5 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("AuctionId must be provided.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidAmountIsZero()
    {
        var bid = new Bid { Id = "1", BidderId = "88", AuctionId = "33", Amount = 0 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("Amount must be greater than 0.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenBidAmountIsNegative()
    {
        var bid = new Bid { Id = "1", BidderId = "45", AuctionId = "12", Amount = -10 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal("Amount must be greater than 0.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenAllFieldsAreValid()
    {
        var bid = new Bid { Id = "1", BidderId = "7", AuctionId = "8", Amount = 100 };

        var exception = Record.Exception(() => bid.Validate());

        Assert.Null(exception);
    }
}