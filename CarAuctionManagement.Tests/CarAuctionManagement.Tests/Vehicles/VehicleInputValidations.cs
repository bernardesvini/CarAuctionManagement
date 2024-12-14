using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Vehicles;
using Moq;

namespace CarAuctionManagement.Tests.Vehicles;

public class VehicleInputValidations
{
    // [Theory]
    // [MemberData(nameof(GetInvalidVehicleData))]
    // public void AddVehicle_ShouldThrowValidationException_WhenInputIsInvalid(
    //     Vehicle vehicle,
    //     string expectedMessage)
    // {
    //     var mockRepository = new Mock<IVehiclesRepository>();
    //     var service = new VehiclesService(mockRepository.Object);
    //
    //     var exception = Assert.Throws<CustomExceptions.ValidationException>(() => service.AddVehicle(vehicle));
    //     Assert.Equal(expectedMessage, exception.Message);
    // }
    //
    // public static IEnumerable<object[]> GetInvalidVehicleData()
    // {
    //     yield return new object[]
    //     {
    //         new Vehicle { Id = null, Manufacturer = "Toyota", Year = 2020 },
    //         "Vehicle id must be provided."
    //     };
    //     yield return new object[]
    //     {
    //         new Vehicle { Id = Guid.NewGuid(), Manufacturer = "", Year = 2020 },
    //         "Manufacturer must be provided."
    //     };
    //     yield return new object[]
    //     {
    //         new Vehicle { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 1800, Model = "Corolla", StartingBid = 1000.00m },
    //         "Year must be a valid."
    //     };
    //     yield return new object[]
    //     {
    //         new Vehicle { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "" },
    //         "Model must be provided."
    //     };
    //     yield return new object[]
    //     {
    //         new Vehicle { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 0 },
    //         "Starting bid must be greater than 0."
    //     };
    //     yield return new object[]
    //     {
    //         new Hatchback { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfDoors = 1 },
    //         "Hatchback number of doors must be greater than 2."
    //     };
    //     yield return new object[]
    //     {
    //         new Sedan { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfDoors = 1 },
    //         "Sedan number of doors must be greater than 2."
    //     };
    //     yield return new object[]
    //     {
    //         new Truck { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 5000.00m, LoadCapacity = 0 },
    //         "Truck load capacity must be greater than 0."
    //     };
    //     yield return new object[]
    //     {
    //         new Truck { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 5000.00m, LoadCapacity = 999999999 },
    //         "Truck load capacity exceeds maximum limit."
    //     };
    //     yield return new object[]
    //     {
    //         new Suv { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 1000.00m, NumberOfSeats = 3 },
    //         "SUV number of seats must be greater than 4."
    //     };
    // }
}