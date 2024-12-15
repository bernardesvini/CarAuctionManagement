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
    private readonly Mock<IAuctionsService> _auctionsServiceMock;
    private readonly Mock<IVehiclesService> _vehiclesServiceMock;
    private readonly StartAuctionHandler _handler;
    
    public StartAuctionHandlerTests()
    {
        _auctionsServiceMock = new Mock<IAuctionsService>();
        _vehiclesServiceMock = new Mock<IVehiclesService>();
        _handler = new StartAuctionHandler(_auctionsServiceMock.Object, _vehiclesServiceMock.Object);
    }
    
    [Fact]
    public void StartAuction_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        var request = new StartAuctionRequestDto { VehicleId = null };
    
        Assert.Throws<ValidationException>(() => _handler.StartAuction(request));
    }
    
    [Fact]
    public void StartAuction_ShouldReturnResponse_WhenRequestIsValid()
    {
        var vehicle = new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 1000.00m);
        var auction = new Auction(Guid.NewGuid(), vehicle, true, new List<Bid?>(), 1000.00m, Guid.NewGuid());
        var request = new StartAuctionRequestDto { VehicleId = vehicle.GetId() };
    
        _vehiclesServiceMock.Setup(v => v.GetVehicleById(It.IsAny<Guid>())).Returns(vehicle);
        _auctionsServiceMock.Setup(a => a.StartAuction(It.IsAny<Auction>())).Returns(auction);
    
        var response = _handler.StartAuction(request);
    
        Assert.NotNull(response);
        Assert.Equal(auction.GetId(), response.Id);
        Assert.Equal(auction.GetHighestBid(), response.HighestBid);
    }
}