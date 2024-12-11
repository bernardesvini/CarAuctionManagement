using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles;

public class VehicleServiceTests
{
    [Fact]
    public void AddVehicle_ShouldBeSuccess()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfDoors = 5 };

        var vehicles = new List<Vehicle?>();
        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        mockRepository.Setup(r => r.AddVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v => vehicles.Add(v));

        service.AddVehicle(vehicle);

        mockRepository.Verify(r => r.AddVehicle(vehicle), Times.Once);
        Assert.Contains(vehicle, vehicles);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenVehicleIsNull()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);

        var exception = Assert.Throws<CustomExceptions.VehicleDataNullException>(() => service.AddVehicle(null));
        Assert.Equal("Vehicle need to have data.", exception.Message);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenVehicleAlreadyExists()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfDoors = 5 };

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        var exception = Assert.Throws<CustomExceptions.VehicleAlreadyExistsException>(() => service.AddVehicle(vehicle));
        Assert.Equal($"Vehicle with ID {vehicle.Id} already exists on the inventory.", exception.Message);
    }

    [Fact]
    public void GetVehicleById_ShouldReturnVehicle_WhenIdIsValid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Tacoma", StartingBid = 1000.00, LoadCapacity = 400 };

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        var result = service.GetVehicleById("1");

        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
    }

    [Fact]
    public void GetVehicleById_ShouldReturnNull_WhenIdIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Suv { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "RAV4", StartingBid = 1000.00, NumberOfSeats = 5 };

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => service.GetVehicleById("2"));
        Assert.Equal($"Vehicle with ID 2 not found.", exception.Message);
    }

    [Fact]
    public void GetVehicleById_ShouldThrowException_WhenNoVehiclesFound()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => service.GetVehicleById("1"));
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Fact]
    public void GetVehicles_ShouldReturnListOfVehicles_WhenVehiclesExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicles = new List<Vehicle?>
        {
            new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfDoors = 5 },
            new Sedan { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 },
            new Truck { Id = "3", Manufacturer = "Honda", Year = 2010, Model = "Glober", StartingBid = 2000.00, LoadCapacity = 500 },
            new Suv { Id = "4", Manufacturer = "Fiat", Year = 1999, Model = "Panda", StartingBid = 500.00, NumberOfSeats = 5 }
        };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = service.GetVehicles();

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void GetVehicles_ShouldThrowException_WhenNoVehiclesExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => service.GetVehicles());
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Fact]
    public void GetVehicleSearch_ShouldReturnFilteredVehicles_WhenMatchingVehiclesExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00, NumberOfDoors = 5 },
            new Truck { Id = "3", Manufacturer = "Honda", Year = 2010, Model = "M3", StartingBid = 2000.00, LoadCapacity = 500 },
            new Suv { Id = "4", Manufacturer = "Fiat", Year = 1999, Model = "Panda", StartingBid = 500.00, NumberOfSeats = 5 }
        };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = service.GetVehicleSearch("Hatchback", "Honda", "Civic", 2019);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("2", result.First()?.Id);
    }

    [Fact]
    public void GetVehicleSearch_ShouldReturnFilteredVehicles_WhenMoreThanOneVehicleMatches()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00, NumberOfDoors = 5 },
            new Hatchback { Id = "3", Manufacturer = "Honda", Year = 2010, Model = "M3", StartingBid = 2000.00, NumberOfDoors = 3 },
            new Suv { Id = "4", Manufacturer = "Honda", Year = 1999, Model = "Jazz", StartingBid = 500.00, NumberOfSeats = 5 }
        };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = service.GetVehicleSearch(null, "Honda");

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetVehicleSearch_ShouldReturnAllVehicles_WhenNoFilterIsApplied()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00, NumberOfDoors = 5 }
        };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var result = service.GetVehicleSearch();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetVehicleSearch_ShouldThrowException_WhenNoMatchingVehiclesExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicles = new List<Vehicle?>
        {
            new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00, NumberOfDoors = 4 },
            new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 900.00, NumberOfDoors = 5 }
        };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundWithFiltersException>(() => service.GetVehicleSearch("SUV", "Ford", "Explorer", 2021));
        Assert.Equal("No vehicles with the specified filters found.", exception.Message);
    }

    [Fact]
    public void UpdateVehicle_ShouldUpdateVehicleDetails_WhenVehicleExists()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var existingVehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Camry", StartingBid = 1000.00, NumberOfDoors = 4 };
        var updatedVehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2021, Model = "Blob", StartingBid = 1000.00, NumberOfDoors = 4 };

        var vehicles = new List<Vehicle?> { existingVehicle };
        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        mockRepository.Setup(r => r.UpdateVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
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

        service.UpdateVehicle(updatedVehicle);

        mockRepository.Verify(r => r.UpdateVehicle(It.Is<Vehicle>(vehicle => vehicle.Year == 2021 && vehicle.Model == "Blob")), Times.Once);
        Assert.Equal(2021, existingVehicle.Year);
        Assert.Equal("Blob", existingVehicle.Model);
    }

    [Fact]
    public void UpdateVehicle_ShouldThrowException_WhenVehicleDoesNotExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var updatedVehicle = new Sedan { Id = "2", Manufacturer = "Toyota", Year = 2021, Model = "Camry", StartingBid = 1200.00, NumberOfDoors = 4 };

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => service.UpdateVehicle(updatedVehicle));
        Assert.Equal("Vehicle with ID 2 not found.", exception.Message);
    }

    [Fact]
    public void UpdateVehicle_ShouldUpdateVehicleTypeAndFields_WhenVehicleTypeIsChanged()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var existingVehicle = new Suv { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "RAV4", StartingBid = 1000.00, NumberOfSeats = 5 };
        var updatedVehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2021, Model = "RAV4", StartingBid = 1200.00, LoadCapacity = 7 };
        var vehicles = new List<Vehicle?> { existingVehicle };

        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);

        mockRepository.Setup(r => r.UpdateVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
        {
            var vehicleToUpdate = vehicles.FirstOrDefault(vehicle => vehicle?.Id == v.Id);
            if (vehicleToUpdate != null)
            {
                var index = vehicles.IndexOf(vehicleToUpdate);
                vehicles[index] = v;
            }
        });

        service.UpdateVehicle(updatedVehicle);

        mockRepository.Verify(r => r.UpdateVehicle(It.Is<Vehicle>(vehicle =>
            vehicle is Truck &&
            vehicle.Manufacturer == "Toyota" &&
            vehicle.Year == 2021 &&
            vehicle.Model == "RAV4" &&
            vehicle.StartingBid == 1200.00 &&
            ((Truck)vehicle).LoadCapacity == 7
        )), Times.Once);


        var updatedStoredVehicle = vehicles.FirstOrDefault();
        Assert.NotNull(updatedStoredVehicle);
        Assert.IsType<Truck>(updatedStoredVehicle);
        Assert.Equal("Toyota", updatedStoredVehicle.Manufacturer);
        Assert.Equal(2021, updatedStoredVehicle.Year);
        Assert.Equal("RAV4", updatedStoredVehicle.Model);
        Assert.Equal(1200.00, updatedStoredVehicle.StartingBid);
        Assert.Equal(7, ((Truck)updatedStoredVehicle).LoadCapacity);
    }
    
    [Fact]
    public void RemoveVehicle_ShouldRemoveVehicle_WhenVehicleExists()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00, NumberOfDoors = 4 };

        var vehicles = new List<Vehicle?> { vehicle };
        mockRepository.Setup(r => r.GetVehicles()).Returns(vehicles);
        mockRepository.Setup(r => r.RemoveVehicle(It.IsAny<Vehicle>())).Callback<Vehicle>(v =>
        {
            var vehicleToRemove = vehicles.FirstOrDefault(veh => veh?.Id == v.Id);
            if (vehicleToRemove != null)
            {
                vehicles.Remove(vehicleToRemove);
            }
        });

        service.RemoveVehicle("1");

        mockRepository.Verify(r => r.RemoveVehicle(It.Is<Vehicle>(v => v.Id == "1")), Times.Once);
        Assert.DoesNotContain(vehicle, vehicles);
    }

    [Fact]
    public void RemoveVehicle_ShouldThrowException_WhenVehicleDoesNotExist()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);

        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.NoVehiclesFoundException>(() => service.RemoveVehicle("2"));
        Assert.Equal("No vehicles found on the inventory.", exception.Message);
    }

    [Fact]
    public void RemoveVehicle_ShouldThrowException_WhenVehicleToRemovedNotFound()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00, NumberOfDoors = 4 };
        var vehicleRemoved = new Hatchback { Id = "2", Manufacturer = "Opel", Year = 2000, Model = "Polo", StartingBid = 1500.00, NumberOfDoors = 4 };
        mockRepository.Setup(r => r.GetVehicles()).Returns(new List<Vehicle?> { vehicle });

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => service.RemoveVehicle(vehicleRemoved.Id));
        Assert.Equal("Vehicle with ID 2 not found.", exception.Message);
    }
}