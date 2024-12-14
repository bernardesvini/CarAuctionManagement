using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.Handlers.Vehicles.RemoveVehicle;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles.Handlers;

public class RemoveVehicleHandlerTests
{
    private readonly Mock<IVehiclesService> _vehiclesServiceMock;
    private readonly RemoveVehicleHandler _handler;

    public RemoveVehicleHandlerTests()
    {
        _vehiclesServiceMock = new Mock<IVehiclesService>();
        _handler = new RemoveVehicleHandler(_vehiclesServiceMock.Object);
    }

    [Fact]
    public void RemoveVehicle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        var request = new VehicleRemoveRequestDto { Id = null };

        Assert.Throws<ValidationException>(() => _handler.RemoveVehicle(request));
    }

    [Fact]
    public void RemoveVehicle_ShouldCallService_WhenRequestIsValid()
    {
        var request = new VehicleRemoveRequestDto { Id = Guid.NewGuid() };

        _handler.RemoveVehicle(request);

        _vehiclesServiceMock.Verify(v => v.RemoveVehicle(request.Id.Value), Times.Once);
    }
}