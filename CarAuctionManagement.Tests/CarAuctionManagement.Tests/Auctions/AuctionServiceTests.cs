using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Auctions;
using Moq;

namespace CarAuctionManagement.Tests.Auctions;

public class AuctionServiceTests
{
    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionAlreadyActive()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "12";
        auction.Object.Vehicle = new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction> { auction.Object });

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        var exception = Assert.Throws<CustomExceptions.AuctionAlreadyActiveException>(() => service.StartAuction(auction.Object));
        Assert.Equal("An auction is already active for vehicle ID 2.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionWithSameIdExists()
    {
        var auction = new Mock<Auction> { CallBase = true };
        var exitingAuction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.Vehicle = new Hatchback { Id = "3", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 };
        exitingAuction.Object.Id = "1";
        exitingAuction.Object.Vehicle = new Hatchback { Id = "1", Manufacturer = "Honda", Year = 2020, Model = "Civic", StartingBid = 1500.00, NumberOfDoors = 4 };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction> { exitingAuction.Object });

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        var exception = Assert.Throws<CustomExceptions.AuctionSameIdException>(() => service.StartAuction(auction.Object));
        Assert.Equal("An auction with the ID 1 already exists.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenVehicleNotFound()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "123";
        auction.Object.Vehicle = new Truck { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, LoadCapacity = 4 };

        var vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
        vehiclesRepositoryMock.Setup(repo => repo.GetVehicles()).Returns(new List<Vehicle?>());

        var service = new AuctionsService(Mock.Of<IAuctionsRepository>(), vehiclesRepositoryMock.Object);

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => service.StartAuction(auction.Object));
        Assert.Equal($"Vehicle with ID 1 not found.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldStartAuction_WhenAuctionIsValid()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.Vehicle = new Suv { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfSeats = 4 };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        var vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
        vehiclesRepositoryMock.Setup(repo => repo.GetVehicles()).Returns(new List<Vehicle?> { auction.Object.Vehicle });

        var service = new AuctionsService(auctionsRepositoryMock.Object, vehiclesRepositoryMock.Object);

        service.StartAuction(auction.Object);

        auctionsRepositoryMock.Verify(repo => repo.StartAuction(auction.Object), Times.Once);
    }

    [Fact]
    public void GetAuctions_ShouldReturnAuctions_WhenAuctionsExist()
    {
        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        var vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
        var auctions = new List<Auction> { new Mock<Auction>().Object };
        auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(auctions);

        var service = new AuctionsService(auctionsRepositoryMock.Object, vehiclesRepositoryMock.Object);

        var result = service.GetAuctions();

        Assert.Equal(auctions, result);
    }

    [Fact]
    public void GetAuctions_ShouldThrowException_WhenNoAuctionsFound()
    {
        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        var vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction>());

        var service = new AuctionsService(auctionsRepositoryMock.Object, vehiclesRepositoryMock.Object);

        var exception = Assert.Throws<CustomExceptions.NoAuctionsFoundException>(() => service.GetAuctions());

        Assert.Equal("No auctions found.", exception.Message);
    }

    [Fact]
    public void EndAuction_ShouldThrowException_WhenAuctionNotFound()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction>());

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        var exception = Assert.Throws<CustomExceptions.AuctionNotFoundException>(() => service.EndAuction(auction.Object));
        Assert.Equal($"Auction with ID 1 not found or isn't active.", exception.Message);
    }

    [Fact]
    public void EndAuction_ShouldEndAuction_WhenAuctionIsActive()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.IsActive = true;

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction.Object });

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        service.EndAuction(auction.Object);

        auctionsRepositoryMock.Verify(repo => repo.EndAuction(auction.Object), Times.Once);
    }

    [Fact]
    public void EndAuction_ShouldSetIsActiveToFalse_WhenAuctionIsActive()
    {
        var auction = new Auction
        {
            Id = "1",
            IsActive = true,
            Vehicle = new Suv { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00, NumberOfSeats = 4 },
            HighestBid = 1500.00,
            HighestBidder = "1"
        };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });
        auctionsRepositoryMock.Setup(repo => repo.EndAuction(It.IsAny<Auction>())).Callback<Auction>(a => a.IsActive = false);

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        service.EndAuction(auction);

        auctionsRepositoryMock.Verify(repo => repo.EndAuction(It.Is<Auction>(a => a.Id == "1")), Times.Once);
        Assert.False(auction.IsActive);
    }

    [Fact]
    public void PlaceBid_ShouldPlaceBid_WhenBidIsValid()
    {
        var auction = new Auction
        {
            Id = "1", HighestBid = 100, IsActive = true, HighestBidder = "2",
            Vehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 50, NumberOfDoors = 4 },
            Bids = new List<Bid?> { new Bid { Id = "1", AuctionId = "1", Amount = 100, BidderId = "1" } }
        };
        var newBid = new Bid { Id = "2", BidderId = "2", AuctionId = "1", Amount = 150 };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });
        auctionsRepositoryMock.Setup(repo => repo.PlaceBid(It.IsAny<Bid>())).Callback<Bid>(bid =>
        {
            auction.HighestBid = bid.Amount;
            auction.HighestBidder = bid.BidderId;
            auction.Bids.Add(bid);
        });

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        service.PlaceBid(newBid);

        auctionsRepositoryMock.Verify(repo => repo.PlaceBid(It.Is<Bid>(a => a.Amount == 150 && a.BidderId == "2")), Times.Once);
        Assert.Equal(150, auction.HighestBid);
        Assert.Equal("2", auction.HighestBidder);
        Assert.Contains(newBid, auction.Bids);
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenAuctionNotFound()
    {
        var newBid = new Bid { AuctionId = "1", Amount = 150, BidderId = "2", Id = "2" };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction>());

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        var exception = Assert.Throws<CustomExceptions.AuctionNotFoundException>(() => service.PlaceBid(newBid));
        Assert.Equal($"Auction with ID 1 not found or isn't active.", exception.Message);
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenBidAmountTooLow()
    {
        var auction = new Auction
        {
            Id = "1", HighestBid = 100, IsActive = true, HighestBidder = "2",
            Vehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 50, NumberOfDoors = 4 },
            Bids = new List<Bid?> { new Bid { Id = "1", AuctionId = "1", Amount = 100, BidderId = "1" } }
        };
        var newBid = new Bid { Id = "3", BidderId = "55", AuctionId = "1", Amount = 80 };

        var auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });

        var service = new AuctionsService(auctionsRepositoryMock.Object, Mock.Of<IVehiclesRepository>());

        var exception = Assert.Throws<CustomExceptions.BidAmountTooLowException>(() => service.PlaceBid(newBid));
        Assert.Equal($"The bid amount 80 is lower or equal to the current highest bid 100.", exception.Message);
    }
}