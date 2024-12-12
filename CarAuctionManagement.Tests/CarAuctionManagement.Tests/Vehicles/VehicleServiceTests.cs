using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles;

public class VehicleServiceTests
{
    private readonly Mock<IVehiclesRepository> _mockRepository;
    private readonly VehiclesService _service;

    public VehicleServiceTests()
    {
        _mockRepository = new Mock<IVehiclesRepository>();
        _service = new VehiclesService(_mockRepository.Object);
    }

    [Fact]
    public void AddVehicle_ShouldBeSuccess()
    {
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfDoors = 5 };
        var vehicles = new List<Vehicle?>();
        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        _mockRepository.Setup(r => r.AddVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v => vehicles.Add(v));

        _service.AddVehicle(vehicle);

        _mockRepository.Verify(r => r.AddVehicle(vehicle), Times.Once);
        Assert.Contains(vehicle, vehicles);
    }

    [Theory]
    [InlineData(null, typeof(CustomExceptions.VehicleDataNullException), "Vehicle need to have data.")]
    [InlineData("1", typeof(CustomExceptions.VehicleAlreadyExistsException), "Vehicle with ID 1 already exists on the inventory.")]
    public void AddVehicle_ShouldThrowException_WhenInvalid(string? id, Type expectedExceptionType, string expectedMessage)
    {
        var vehicle = id == null ? null : new Hatchback { Id = id, Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfDoors = 5 };
        if (id != null)
        {
            _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });
        }

        var exception = Assert.Throws(expectedExceptionType, () => _service.AddVehicle(vehicle));
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData("1", true)]
    [InlineData("2", false)]
    public void GetVehicleById_ShouldReturnCorrectResult(string id, bool shouldExist)
    {
        var vehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Tacoma", StartingBid = 1000.00m, LoadCapacity = 400 };
        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        if (shouldExist)
        {
            var result = _service.GetVehicleById(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
        else
        {
            var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => _service.GetVehicleById(id));
            Assert.Equal($"Vehicle with ID {id} not found.", exception.Message);
        }
    }

    [Fact]
    public void GetVehicleById_ShouldThrowException_WhenNoVehiclesFound()
    {
        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => _service.GetVehicleById("1"));
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Fact]
    public void GetVehicles_ShouldReturnListOfVehicles_WhenVehiclesExist()
    {
        var vehicles = new List<Vehicle?>
        {
            new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfDoors = 5 },
            new Sedan { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfDoors = 4 },
            new Truck { Id = "3", Manufacturer = "Honda", Year = 2010, Model = "Glober", StartingBid = 2000.00m, LoadCapacity = 500 },
            new Suv { Id = "4", Manufacturer = "Fiat", Year = 1999, Model = "Panda", StartingBid = 500.00m, NumberOfSeats = 5 }
        };

        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = _service.GetVehicles();

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void GetVehicles_ShouldThrowException_WhenNoVehiclesExist()
    {
        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => _service.GetVehicles());
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Theory]
    [InlineData("Hatchback", "Honda", "Civic", 2019, 1)]
    [InlineData(null, "Honda", null, null, 3)]
    [InlineData(null, null, null, null, 4)]
    public void GetVehicleSearch_ShouldReturnFilteredVehicles(string type, string manufacturer, string model, int? year, int expectedCount)
    {
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00m, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00m, NumberOfDoors = 5 },
            new Hatchback { Id = "3", Manufacturer = "Honda", Year = 2010, Model = "M3", StartingBid = 2000.00m, NumberOfDoors = 3 },
            new Suv { Id = "4", Manufacturer = "Honda", Year = 1999, Model = "Jazz", StartingBid = 500.00m, NumberOfSeats = 5 }
        };

        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = _service.GetVehicleSearch(type, manufacturer, model, year);

        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void GetVehicleSearch_ShouldThrowException_WhenNoMatchingVehiclesExist()
    {
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00m, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00m, NumberOfDoors = 5 }
        };

        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundWithFiltersException>(() => _service.GetVehicleSearch("SUV", "Ford", "Explorer", 2021));
        Assert.Equal("No vehicles with the specified filters found.", exception.Message);
    }

    [Fact]
    public void UpdateVehicle_ShouldUpdateVehicleDetails_WhenVehicleExists()
    {
        var existingVehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00m, NumberOfDoors = 4 };
        var updatedVehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2021, Model = "Blob", StartingBid = 1000.00m, NumberOfDoors = 4 };

        var vehicles = new List<Vehicle?> { existingVehicle };
        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        _mockRepository.Setup(r => r.UpdateVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
        {
            var vehicleToUpdate = vehicles.FirstOrDefault(vehicle => vehicle?.Id == v.Id);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.Manufacturer = v.Manufacturer;
                vehicleToUpdate.Year = v.Year;
                vehicleToUpdate.Model = v.Model;
                vehicleToUpdate.StartingBid = v.StartingBid;
            }
        });

        _service.UpdateVehicle(updatedVehicle);

        _mockRepository.Verify(r => r.UpdateVehicle(It.Is<Vehicle>(vehicle => vehicle.Year == 2021 && vehicle.Model == "Blob")), Times.Once);
        Assert.Equal(2021, existingVehicle.Year);
        Assert.Equal("Blob", existingVehicle.Model);
    }

    [Fact]
    public void UpdateVehicle_ShouldThrowException_WhenVehicleDoesNotExist()
    {
        var updatedVehicle = new Sedan { Id = "2", Manufacturer = "Toyota", Year = 2021, Model = "Camry", StartingBid = 1200.00m, NumberOfDoors = 4 };

        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => _service.UpdateVehicle(updatedVehicle));
        Assert.Equal("Vehicle with ID 2 not found.", exception.Message);
    }

    [Fact]
    public void UpdateVehicle_ShouldUpdateVehicleTypeAndFields_WhenVehicleTypeIsChanged()
    {
        var existingVehicle = new Suv { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "RAV4", StartingBid = 1000.00m, NumberOfSeats = 5 };
        var updatedVehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2021, Model = "RAV4", StartingBid = 1200.00m, LoadCapacity = 7 };
        var vehicles = new List<Vehicle?> { existingVehicle };

        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        _mockRepository.Setup(r => r.UpdateVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
        {
            var vehicleToUpdate = vehicles.FirstOrDefault(vehicle => vehicle?.Id == v.Id);
            if (vehicleToUpdate != null)
            {
                var index = vehicles.IndexOf(vehicleToUpdate);
                vehicles[index] = v;
            }
        });

        _service.UpdateVehicle(updatedVehicle);

        _mockRepository.Verify(r => r.UpdateVehicle(It.Is<Vehicle>(vehicle =>
            vehicle is Truck &&
            vehicle.Manufacturer == "Toyota" &&
            vehicle.Year == 2021 &&
            vehicle.Model == "RAV4" &&
            vehicle.StartingBid == 1200.00m &&
            ((Truck)vehicle).LoadCapacity == 7
        )), Times.Once);

        var updatedStoredVehicle = vehicles.FirstOrDefault();
        Assert.NotNull(updatedStoredVehicle);
        Assert.IsType<Truck>(updatedStoredVehicle);
        Assert.Equal("Toyota", updatedStoredVehicle.Manufacturer);
        Assert.Equal(2021, updatedStoredVehicle.Year);
        Assert.Equal("RAV4", updatedStoredVehicle.Model);
        Assert.Equal(1200.00m, updatedStoredVehicle.StartingBid);
        Assert.Equal(7, ((Truck)updatedStoredVehicle).LoadCapacity);
    }

    [Fact]
    public void RemoveVehicle_ShouldRemoveVehicle_WhenVehicleExists()
    {
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00m, NumberOfDoors = 4 };
        var vehicles = new List<Vehicle?> { vehicle };
        _mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        _mockRepository.Setup(r => r.RemoveVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
        {
            var vehicleToRemove = vehicles.FirstOrDefault(veh => veh?.Id == v.Id);
            if (vehicleToRemove != null)
            {
                vehicles.Remove(vehicleToRemove);
            }
        });

        _service.RemoveVehicle("1");

        _mockRepository.Verify(r => r.RemoveVehicle(It.Is<Vehicle>(v => v.Id == "1")), Times.Once);
        Assert.DoesNotContain(vehicle, vehicles);
    }

    [Fact]
    public void RemoveVehicle_ShouldThrowException_WhenVehicleDoesNotExist()
    {
        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => _service.RemoveVehicle("2"));
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Fact]
    public void RemoveVehicle_ShouldThrowException_WhenVehicleToRemovedNotFound()
    {
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00m, NumberOfDoors = 4 };
        var vehicleRemoved = new Hatchback { Id = "2", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00m, NumberOfDoors = 4 };
        _mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => _service.RemoveVehicle(vehicleRemoved.Id));
        Assert.Equal("Vehicle with ID 2 not found.", exception.Message);
    }
}