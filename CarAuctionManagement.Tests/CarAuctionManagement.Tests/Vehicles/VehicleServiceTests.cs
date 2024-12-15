using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles
{
    public class VehiclesServiceTests
    {
        private readonly Mock<IVehiclesRepository> _vehiclesRepositoryMock;
        private readonly Mock<IAuctionsRepository> _auctionsRepositoryMock;
        private readonly VehiclesService _service;

        public VehiclesServiceTests()
        {
            _vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
            _auctionsRepositoryMock = new Mock<IAuctionsRepository>();
            _service = new VehiclesService(_vehiclesRepositoryMock.Object, _auctionsRepositoryMock.Object);
        }

        [Fact]
        public void AddVehicle_ShouldAddVehicle_WhenVehicleIsValid()
        {
            var vehicle = new Sedan ( Guid.NewGuid(), "Toyota", "Camry", 2020, 1000.00m,  4 );
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());
            _vehiclesRepositoryMock.Setup(r => r.AddVehicle(vehicle)).Returns(vehicle);

            var result = _service.AddVehicle(vehicle);

            Assert.NotNull(result);
            Assert.Equal(vehicle.GetId(), result?.GetId());
            _vehiclesRepositoryMock.Verify(r => r.AddVehicle(vehicle), Times.Once);
        }

        [Fact]
        public void GetVehicleById_ShouldReturnVehicle_WhenVehicleExists()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = new Hatchback ( vehicleId, "Toyota", "Camry", 2020, 1000.00m,  4 );
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

            var result = _service.GetVehicleById(vehicleId);

            Assert.NotNull(result);
            Assert.Equal(vehicleId, result.GetId());
        }

        [Fact]
        public void GetVehicleById_ShouldThrowNotFoundException_WhenVehicleDoesNotExist()
        {
            var vehicleId = Guid.NewGuid();
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

            Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => _service.GetVehicleById(vehicleId));
        }

        [Fact]
        public void GetVehicles_ShouldReturnAllVehicles()
        {
            var vehicles = new List<Vehicle?> { new Truck( Guid.NewGuid(), "Toyota", "Corolla", 2020, 1000.00m,  4 )};
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(vehicles);

            var result = _service.GetVehicles();

            Assert.NotNull(result);
            Assert.Equal(vehicles.Count, result.Count);
        }

        [Fact]
        public void UpdateVehicle_ShouldUpdateVehicle_WhenVehicleIsValid()
        {
            var vehicleId = Guid.NewGuid();
            var existingVehicle = new Hatchback ( vehicleId, "Toyota", "Camry", 2020, 1000.00m,  4 );
            var updatedVehicle = new Hatchback( vehicleId, "Hyundai", "Santa Fé", 2021, 1200.00m,  4 );
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { existingVehicle });
            _vehiclesRepositoryMock.Setup(r => r.UpdateVehicle(updatedVehicle)).Returns(updatedVehicle);

            var result = _service.UpdateVehicle(updatedVehicle);

            Assert.NotNull(result);
            Assert.Equal(2021, result?.GetYear());
            Assert.Equal(1200.00m, result?.GetStartingBid());
        }

        [Fact]
        public void RemoveVehicle_ShouldRemoveVehicle_WhenVehicleExists()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = new Hatchback ( vehicleId, "Toyota", "Camry", 2020, 1000.00m,  4 );
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

            _service.RemoveVehicle(vehicleId);

            _vehiclesRepositoryMock.Verify(r => r.RemoveVehicle(vehicleId), Times.Once);
        }

        [Fact]
        public void RemoveVehicle_ShouldThrowNotFoundException_WhenVehicleDoesNotExist()
        {
            var vehicleId = Guid.NewGuid();
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

            Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => _service.RemoveVehicle(vehicleId));
        }

        [Fact]
        public void GetVehicleSearch_ShouldReturnFilteredVehicles()
        {
            var vehicles = new List<Vehicle?> { new Sedan ( Guid.NewGuid(), "Toyota", "Camry", 2020, 1000.00m,  4 ) };
            _vehiclesRepositoryMock.Setup(r => r.GetVehicles()).Returns(vehicles);

            var result = _service.GetVehicleSearch("Sedan", "Toyota", "Camry", 2020, 2021);

            Assert.NotNull(result);
            Assert.Single(result);
        }
        
    }
}