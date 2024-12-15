using CarAuctionManagement.DTOs.Enums;
using CarAuctionManagement.Handlers.Vehicles.GetVehicles;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles.Handlers
{
    public class GetVehiclesHandlerTests
    {
        private readonly Mock<IVehiclesService> _vehiclesServiceMock;
        private readonly GetVehiclesHandler _handler;

        public GetVehiclesHandlerTests()
        {
            _vehiclesServiceMock = new Mock<IVehiclesService>();
            _handler = new GetVehiclesHandler(_vehiclesServiceMock.Object);
        }

        [Theory]
        [InlineData(2020, 2021, null, VehicleType.Sedan, "Toyota", "Corolla", 1, 10)]
        [InlineData(2020, default, null, VehicleType.Sedan, "Toyota", "Corolla", 1, 10)]
        [InlineData(2020, 2021, null, null, "Toyota", "Corolla", 1, 10)]
        [InlineData(default, default, null, null, null, null, 1, 10)]
        public void GetVehiclesWithFilters_ShouldReturnFilteredVehicles(int? startYear, int? endYear, Guid? id, VehicleType? type, string manufacturer, string model, int page, int pageSize)
        {
            var vehicles = new List<Vehicle?> { new Sedan( Guid.NewGuid(), "Toyota", "Corolla", 2020, 1250.50m, 4  ) };
            _vehiclesServiceMock.Setup(v => v.GetVehicleSearch(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Guid?>())).Returns(vehicles);

            var result = _handler.GetVehiclesWithFilters(startYear, endYear, id, type, manufacturer, model, page, pageSize);

            Assert.NotNull(result);
            Assert.Equal(vehicles.Count, result.VehiclesList?.Count);
        }

        [Theory]
        [InlineData(1800, "Toyota", "Corolla")]
        public void GetVehiclesWithFilters_ShouldThrowValidationException_WhenFiltersAreInvalid(int? year,  string? manufacturer, string? model)
        {
            Assert.Throws<ValidationException>(() => _handler.GetVehiclesWithFilters(year, null, null, null, manufacturer, model, 1, 10));
        }
    }
}