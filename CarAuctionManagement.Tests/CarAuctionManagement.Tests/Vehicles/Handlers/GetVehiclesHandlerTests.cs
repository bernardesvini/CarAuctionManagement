using CarAuctionManagement.Handlers.Vehicles.GetVehicles;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles.Handlers;

public class GetVehiclesHandlerTests
{
    private readonly Mock<IVehiclesService> _vehiclesServiceMock;
    private readonly GetVehiclesHandler _handler;

    public GetVehiclesHandlerTests()
    {
        _vehiclesServiceMock = new Mock<IVehiclesService>();
        _handler = new GetVehiclesHandler(_vehiclesServiceMock.Object);
    }

    [Fact]
    public void GetVehicles_ShouldReturnListOfVehicles()
    {
        var vehicles = new List<Vehicle?> { new Sedan { Id = Guid.NewGuid() } };

        _vehiclesServiceMock.Setup(v => v.GetVehicles()).Returns(vehicles);

        var result = _handler.GetVehicles();

        Assert.NotNull(result);
        Assert.Equal(vehicles.Count, result.GetVehicles.Count);
    }

    [Theory]
    [InlineData(2020, "Sedan", "Toyota", "Corolla")]
    [InlineData(null, null, null, null)]
    public void GetVehiclesWithFilters_ShouldReturnFilteredVehicles(int? year, string? type, string? manufacturer, string? model)
    {
        var vehicles = new List<Vehicle?> { new Sedan { Id = Guid.NewGuid() } };

        _vehiclesServiceMock.Setup(v => v.GetVehicleSearch(type, manufacturer, model, year)).Returns(vehicles);

        var result = _handler.GetVehiclesWithFilters(year, type, manufacturer, model);

        Assert.NotNull(result);
        Assert.Equal(vehicles.Count, result.GetVehicles.Count);
    }

    [Theory]
    [InlineData(1800, "InvalidType", "Toyota", "Corolla")]
    public void GetVehiclesWithFilters_ShouldThrowValidationException_WhenFiltersAreInvalid(int? year, string? type, string? manufacturer, string? model)
    {
        Assert.Throws<ValidationException>(() => _handler.GetVehiclesWithFilters(year, type, manufacturer, model));
    }
}