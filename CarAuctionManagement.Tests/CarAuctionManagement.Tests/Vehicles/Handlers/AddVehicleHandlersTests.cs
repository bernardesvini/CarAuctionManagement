using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Handlers.Vehicles.AddVehicle;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles.Handlers
{
    public class AddVehicleHandlerTests
    {
        private readonly Mock<IVehiclesService> _vehiclesServiceMock;
        private readonly AddVehicleHandler _handler;

        public AddVehicleHandlerTests()
        {
            _vehiclesServiceMock = new Mock<IVehiclesService>();
            _handler = new AddVehicleHandler(_vehiclesServiceMock.Object);
        }

        [Theory]
        [InlineData("Toyota", "Corolla", 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, default, default)]
        [InlineData("Ford", "F-150", 2021, 2000.00, DTOs.Enums.VehicleType.Truck, default, default, 5000.0)]
        public void AddVehicle_ShouldReturnResponse_WhenRequestIsValid(
            string manufacturer, string model, int year, double? startingBid, DTOs.Enums.VehicleType? type, int? numberOfDoors, int? numberOfSeats, double? loadCapacity)
        {
            var request = new VehicleRequestDto
            {
                Manufacturer = manufacturer,
                Model = model,
                Year = year,
                StartingBid = startingBid.HasValue ? (decimal)startingBid : null,
                Type = type,
                NumberOfDoors = numberOfDoors,
                NumberOfSeats = numberOfSeats,
                LoadCapacity = loadCapacity.HasValue ? (decimal)loadCapacity : null
            };

            var vehicle = new Vehicle
            (
                Guid.NewGuid(),
                request.Manufacturer,
                request.Model,
                request.Year,
                request.StartingBid
            );

            _vehiclesServiceMock.Setup(v => v.AddVehicle(It.IsAny<Vehicle>())).Returns(vehicle);

            var response = _handler.AddVehicle(request);

            Assert.NotNull(response);
            Assert.Equal(vehicle.GetId(), response.Id);
            Assert.Equal(vehicle.GetManufacturer(), response.Manufacturer);
            Assert.Equal(vehicle.GetModel(), response.Model);
            Assert.Equal(vehicle.GetYear(), response.Year);
            Assert.Equal(vehicle.GetStartingBid(), response.StartingBid);
        }

        [Theory]
        [InlineData(null, "Model", 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, default, default)]
        [InlineData("Manufacturer", null, 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, default, default)]
        [InlineData("Manufacturer", "Model", 1800, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, default, default)]
        public void AddVehicle_ShouldThrowValidationException_WhenRequestIsInvalid(
            string manufacturer, string model, int year, double? startingBid, DTOs.Enums.VehicleType? type, int? numberOfDoors, int? numberOfSeats, double? loadCapacity)
        {
            var request = new VehicleRequestDto
            {
                Manufacturer = manufacturer,
                Model = model,
                Year = year,
                StartingBid = startingBid.HasValue ? (decimal)startingBid : null,
                Type = type,
                NumberOfDoors = numberOfDoors,
                NumberOfSeats = numberOfSeats,
                LoadCapacity = loadCapacity.HasValue ? (decimal)loadCapacity : null
            };

            Assert.Throws<ValidationException>(() => _handler.AddVehicle(request));
        }

        [Fact]
        public void UpdateVehicle_ShouldReturnResponse_WhenRequestIsValid()
        {
            var id = Guid.NewGuid();
            var request = new VehicleUpdateRequestDto
            {
                Manufacturer = "Toyota",
                Model = "Corolla",
                Year = 2020,
                StartingBid = 1000.00m,
                Type = DTOs.Enums.VehicleType.Sedan,
                NumberOfDoors = 4
            };

            var existingVehicle = new Sedan
            (
                id,
                "Toyota",
                "Corolla",
                2019,
                900.00m,
                4
            );

            _vehiclesServiceMock.Setup(v => v.GetVehicleById(id)).Returns(existingVehicle);
            _vehiclesServiceMock.Setup(v => v.UpdateVehicle(It.IsAny<Vehicle>())).Returns(existingVehicle);

            var response = _handler.UpdateVehicle(id, request);

            Assert.NotNull(response);
            Assert.Equal(existingVehicle.GetId(), response.Id);
            Assert.Equal(existingVehicle.GetManufacturer(), response.Manufacturer);
            Assert.Equal(existingVehicle.GetModel(), response.Model);
            Assert.Equal(existingVehicle.GetYear(), response.Year);
            Assert.Equal(existingVehicle.GetStartingBid(), response.StartingBid);
            Assert.Equal(existingVehicle.GetNumberOfDoors(), response.NumberOfDoors);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000",null, "Model", 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 0, default, default)]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6","Manufacturer", null, 2020, 1000.00, DTOs.Enums.VehicleType.Sedan, 4, default, default)]
        public void UpdateVehicle_ShouldThrowValidationException_WhenRequestIsInvalid(
            Guid id, string manufacturer, string model, int year, double? startingBid, DTOs.Enums.VehicleType type, int? numberOfDoors, int? numberOfSeats, double? loadCapacity)
        {
            
            var request = new VehicleUpdateRequestDto
            {
                Manufacturer = manufacturer,
                Model = model,
                Year = year,
                StartingBid = startingBid.HasValue ? (decimal)startingBid : null,
                Type = type,
                NumberOfDoors = numberOfDoors,
                NumberOfSeats = numberOfSeats,
                LoadCapacity = loadCapacity.HasValue ? (decimal)loadCapacity : null
            };

            Assert.Throws<CustomExceptions.ValidationException>(() => _handler.UpdateVehicle(id, request));
        }
        
        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6","Manufacturer", "Model", 1800, 1000.00, DTOs.Enums.VehicleType.Truck, 4, default, default)]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6","Manufacturer", "Model", 1800, 1000.00, DTOs.Enums.VehicleType.Truck, 4, default, 1000001.0)]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6","Manufacturer", "Model", 1800, 1000.00, DTOs.Enums.VehicleType.Suv, 4, default, 1000001.0)]
        public void UpdateVehicle_ShouldThrowValidationException_WhenRequestIsInvalidFluentValidation(
            Guid id, string manufacturer, string model, int year, double? startingBid, DTOs.Enums.VehicleType type, int? numberOfDoors, int? numberOfSeats, double? loadCapacity)
        {
            
            var request = new VehicleUpdateRequestDto
            {
                Manufacturer = manufacturer,
                Model = model,
                Year = year,
                StartingBid = startingBid.HasValue ? (decimal)startingBid : null,
                Type = type,
                NumberOfDoors = numberOfDoors,
                NumberOfSeats = numberOfSeats,
                LoadCapacity = loadCapacity.HasValue ? (decimal)loadCapacity : null
            };

            Assert.Throws<ValidationException>(() => _handler.UpdateVehicle(id, request));
        }
    }
}
