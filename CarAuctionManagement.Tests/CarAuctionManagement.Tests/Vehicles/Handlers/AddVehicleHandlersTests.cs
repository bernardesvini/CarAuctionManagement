using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.Handlers.Vehicles.AddVehicle;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles.Handlers;

public class AddVehicleHandlerTests
{
    // private readonly Mock<IVehiclesService> _vehiclesServiceMock;
    // private readonly AddVehicleHandler _handler;
    //
    // public AddVehicleHandlerTests()
    // {
    //     _vehiclesServiceMock = new Mock<IVehiclesService>();
    //     _handler = new AddVehicleHandler(_vehiclesServiceMock.Object);
    // }
    //
    // [Theory]
    // [InlineData(null, "Model", 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, null, null)]
    // [InlineData("Manufacturer", null, 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, null, null)]
    // [InlineData("Manufacturer", "Model", 1800, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, null, null)]
    // public void AddVehicle_ShouldThrowValidationException_WhenRequestIsInvalid(
    //     string manufacturer, string model, int year, decimal startingBid, DTOs.Enums.VehicleType type, int? numberOfDoors, int? numberOfSeats, decimal? loadCapacity)
    // {
    //     var request = new VehicleRequestDto
    //     {
    //         Manufacturer = manufacturer,
    //         Model = model,
    //         Year = year,
    //         StartingBid = startingBid,
    //         Type = type,
    //         NumberOfDoors = numberOfDoors,
    //         NumberOfSeats = numberOfSeats,
    //         LoadCapacity = loadCapacity
    //     };
    //
    //     Assert.Throws<ValidationException>(() => _handler.AddVehicle(request));
    // }
    //
    // [Fact]
    // public void AddVehicle_ShouldReturnResponse_WhenRequestIsValid()
    // {
    //     var request = new VehicleRequestDto
    //     {
    //         Manufacturer = "Toyota",
    //         Model = "Corolla",
    //         Year = 2020,
    //         StartingBid = 1000.00m,
    //         Type = DTOs.Enums.VehicleType.Sedan,
    //         NumberOfDoors = 4
    //     };
    //
    //     var vehicle = new Sedan
    //     {
    //         Id = Guid.NewGuid(),
    //         Manufacturer = request.Manufacturer,
    //         Model = request.Model,
    //         Year = request.Year,
    //         StartingBid = request.StartingBid,
    //         NumberOfDoors = request.NumberOfDoors
    //     };
    //
    //     _vehiclesServiceMock.Setup(v => v.AddVehicle(It.IsAny<Vehicle>())).Returns(vehicle);
    //
    //     var response = _handler.AddVehicle(request);
    //
    //     Assert.NotNull(response);
    //     Assert.Equal(vehicle.Id, response.Id);
    //     Assert.Equal(vehicle.Manufacturer, response.Manufacturer);
    //     Assert.Equal(vehicle.Model, response.Model);
    //     Assert.Equal(vehicle.Year, response.Year);
    //     Assert.Equal(vehicle.StartingBid, response.StartingBid);
    //     Assert.Equal(DTOs.Enums.VehicleType.Sedan, response.Type);
    //     Assert.Equal(vehicle.NumberOfDoors, response.NumberOfDoors);
    // }
}