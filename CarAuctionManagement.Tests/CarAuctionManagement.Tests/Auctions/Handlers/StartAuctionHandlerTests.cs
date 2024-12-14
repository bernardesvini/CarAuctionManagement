using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.Handlers.Auctions.StartAuction;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Auctions.Handlers;

public class StartAuctionHandlerTests
{
    // private readonly Mock<IAuctionsService> _auctionsServiceMock;
    // private readonly Mock<IVehiclesService> _vehiclesServiceMock;
    // private readonly StartAuctionHandler _handler;
    //
    // public StartAuctionHandlerTests()
    // {
    //     _auctionsServiceMock = new Mock<IAuctionsService>();
    //     _vehiclesServiceMock = new Mock<IVehiclesService>();
    //     _handler = new StartAuctionHandler(_auctionsServiceMock.Object, _vehiclesServiceMock.Object);
    // }
    //
    // [Fact]
    // public void StartAuction_ShouldThrowValidationException_WhenRequestIsInvalid()
    // {
    //     var request = new StartAuctionRequestDto { VehicleId = null };
    //
    //     Assert.Throws<ValidationException>(() => _handler.StartAuction(request));
    // }
    //
    // [Fact]
    // public void StartAuction_ShouldReturnResponse_WhenRequestIsValid()
    // {
    //     var vehicle = new Vehicle { Id = Guid.NewGuid(), StartingBid = 1000.00m };
    //     var auction = new Auction { Id = Guid.NewGuid(), Vehicle = vehicle, HighestBid = 1000.00m };
    //     var request = new StartAuctionRequestDto { VehicleId = vehicle.Id };
    //
    //     _vehiclesServiceMock.Setup(v => v.GetVehicleById(vehicle.Id)).Returns(vehicle);
    //     _auctionsServiceMock.Setup(a => a.StartAuction(It.IsAny<Auction>())).Returns(auction);
    //
    //     var response = _handler.StartAuction(request);
    //
    //     Assert.NotNull(response);
    //     Assert.Equal(auction.Id, response.Id);
    //     Assert.Equal(auction.Vehicle, response.Vehicle);
    //     Assert.Equal(auction.HighestBid, response.HighestBid);
    // }
}