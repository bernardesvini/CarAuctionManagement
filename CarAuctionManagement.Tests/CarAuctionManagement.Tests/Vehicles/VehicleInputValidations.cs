using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles;

public class VehicleInputValidations
{
    [Fact]
    public void AddVehicle_ShouldThrowException_WhenVehicleIdIsEmpty()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Vehicle { Id = "", Manufacturer = "Toyota", Year = 2020 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Vehicle id must be provided.", exception.Message);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenManufacturerIsEmpty()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Vehicle { Id = "1", Manufacturer = "", Year = 2020 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Manufacturer must be provided.", exception.Message);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenYearIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Vehicle { Id = "1", Manufacturer = "Toyota", Year = 1800, Model = "Corolla", StartingBid = 1000.00 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Year must be a valid.", exception.Message);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenModelIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Vehicle { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "" };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Model must be provided.", exception.Message);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenStartingBidIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Vehicle { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 0 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Starting bid must be greater than 0.", exception.Message);
    }

    [Fact]
    public void AddVehicleHatchback_ShouldThrowException_WhenNumberOfDoorsIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Hatchback { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfDoors = 2 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Hatchback number of doors must be greater than 3.", exception.Message);
    }

    [Fact]
    public void AddVehicleSedan_ShouldThrowException_WhenStartingBidIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfDoors = 2 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Sedan number of doors must be greater than 3.", exception.Message);
    }

    [Fact]
    public void AddVehicleTruck_ShouldThrowException_WhenLoadCapacityIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 5000.00, LoadCapacity = 0 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Truck load capacity must be greater than 0.", exception.Message);
    }

    [Fact]
    public void AddVehicleTruck_ShouldThrowException_WhenLoadCapacityExcedessLimits()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Truck { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 5000.00, LoadCapacity = 999999999 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("Truck load capacity exceeds maximum limit.", exception.Message);
    }

    [Fact]
    public void AddVehicleSuv_ShouldThrowException_WhenNumberOfSeatsIsInvalid()
    {
        var mockRepository = new Mock<IVehiclesRepository>();
        var service = new VehiclesService(mockRepository.Object);
        var vehicle = new Suv { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00, NumberOfSeats = 3 };

        var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
        Assert.Equal("SUV number of seats must be greater than 4.", exception.Message);
    }
}