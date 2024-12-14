using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Auctions;

public class AuctionsInputValidation
{
    // public static IEnumerable<object?[]> GetInvalidAuctionData()
    // {
    //     yield return new object?[] { null, new Sedan { Id = Guid.NewGuid(), Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfDoors = 4 }, "Id must be provided." };
    //     yield return new object?[] { Guid.NewGuid(), null, "Vehicle must be provided." };
    // }
    //
    // [Theory]
    // [MemberData(nameof(GetInvalidAuctionData))]
    // public void Validate_ShouldThrowException_WithInvalidAuction(Guid id, Vehicle vehicle, string expectedMessage)
    // {
    //     var auction = new Mock<Auction> { CallBase = true };
    //     auction.Object.Id = id;
    //     auction.Object.Vehicle = vehicle;
    //
    //     var exception = Assert.Throws<CustomExceptions.ValidationException>(() => auction.Object.Validate());
    //     Assert.Equal(expectedMessage, exception.Message);
    // }
    //
    // [Fact]
    // public void Validate_ShouldCallVehicleValidate_WhenVehicleIsNotNull()
    // {
    //     var vehicleMock = new Mock<Truck>();
    //     var auction = new Mock<Auction> { CallBase = true };
    //     auction.Object.Id = Guid.NewGuid();
    //     auction.Object.Vehicle = vehicleMock.Object;
    //
    //     auction.Object.Validate();
    //
    //     vehicleMock.Verify(v => v.Validate(), Times.Once);
    // }
    //
    // public static IEnumerable<object?[]> GetInvalidBidData()
    // {
    //     yield return new object?[] { null, "55", Guid.NewGuid(), 50, "Id must be provided." };
    //     yield return new object?[] { Guid.NewGuid(), null, Guid.NewGuid(), 2000, "BidderId must be provided." };
    //     yield return new object?[] { Guid.NewGuid(), "", Guid.NewGuid(), 100, "BidderId must be provided." };
    //     yield return new object?[] { Guid.NewGuid(), "25", null, 1000, "AuctionId must be provided." };
    //     yield return new object?[] { Guid.NewGuid(), "88", Guid.NewGuid(), 0, "Amount must be greater than 0." };
    //     yield return new object?[] { Guid.NewGuid(), "45", Guid.NewGuid(), -10, "Amount must be greater than 0." };
    // }
    //
    // [Theory]
    // [MemberData(nameof(GetInvalidBidData))]
    // public void Validate_ShouldThrowException_WithInvalidBid(Guid id, string bidderId, Guid auctionId, decimal amount, string expectedMessage)
    // {
    //     var bid = new Bid { Id = id, BidderId = bidderId, AuctionId = auctionId, Amount = amount };
    //
    //     var exception = Assert.Throws<CustomExceptions.ValidationException>(() => bid.Validate());
    //     Assert.Equal(expectedMessage, exception.Message);
    // }
    //
    // [Fact]
    // public void Validate_ShouldNotThrowException_WhenAllFieldsAreValid()
    // {
    //     var bid = new Bid { Id = Guid.NewGuid(), BidderId = "7", AuctionId = Guid.NewGuid(), Amount = 100 };
    //
    //     var exception = Record.Exception(() => bid.Validate());
    //
    //     Assert.Null(exception);
    // }
}