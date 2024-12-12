using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Auctions;

public class AuctionsInputValidation
{
    public static IEnumerable<object[]> GetInvalidAuctionData()
    {
        yield return new object[] { null, new Sedan { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 }, "Id must be provided." };
        yield return new object[] { "", new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 }, "Id must be provided." };
        yield return new object[] { "1", null, "Vehicle must be provided." };
    }

    [Theory]
    [MemberData(nameof(GetInvalidAuctionData))]
    public void Validate_ShouldThrowException_WithInvalidAuction(string id, Vehicle vehicle, string expectedMessage)
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = id;
        auction.Object.Vehicle = vehicle;

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => auction.Object.Validate());
        Assert.Equal(expectedMessage, exception.Message);
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

    public static IEnumerable<object[]> GetInvalidBidData()
    {
        yield return new object[] { null, "55", "456", 50, "Id must be provided." };
        yield return new object[] { "", "2", "13", 1589, "Id must be provided." };
        yield return new object[] { "1", null, "456", 2000, "BidderId must be provided." };
        yield return new object[] { "1", "", "456", 100, "BidderId must be provided." };
        yield return new object[] { "1", "25", null, 1000, "AuctionId must be provided." };
        yield return new object[] { "1", "88", "", 5, "AuctionId must be provided." };
        yield return new object[] { "1", "88", "33", 0, "Amount must be greater than 0." };
        yield return new object[] { "1", "45", "12", -10, "Amount must be greater than 0." };
    }

    [Theory]
    [MemberData(nameof(GetInvalidBidData))]
    public void Validate_ShouldThrowException_WithInvalidBid(string id, string bidderId, string auctionId, double amount, string expectedMessage)
    {
        var bid = new Bid { Id = id, BidderId = bidderId, AuctionId = auctionId, Amount = amount };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenAllFieldsAreValid()
    {
        var bid = new Bid { Id = "1", BidderId = "7", AuctionId = "8", Amount = 100 };

        var exception = Record.Exception(() => bid.Validate());

        Assert.Null(exception);
    }
}