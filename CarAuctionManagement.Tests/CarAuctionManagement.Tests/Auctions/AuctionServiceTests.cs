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
    private readonly Mock<IAuctionsRepository> _auctionsRepositoryMock;
    private readonly Mock<IVehiclesRepository> _vehiclesRepositoryMock;
    private readonly AuctionsService _service;

    public AuctionServiceTests()
    {
        _auctionsRepositoryMock = new Mock<IAuctionsRepository>();
        _vehiclesRepositoryMock = new Mock<IVehiclesRepository>();
        _service = new AuctionsService(_auctionsRepositoryMock.Object, _vehiclesRepositoryMock.Object);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionAlreadyActive()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "12";
        auction.Object.Vehicle = new Hatchback { Id = "2", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfDoors = 4 };

        _auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction> { auction.Object });

        var exception = Assert.Throws<CustomExceptions.AuctionAlreadyActiveException>(() => _service.StartAuction(auction.Object));
        Assert.Equal("An auction is already active for vehicle ID 2.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionWithSameIdExists()
    {
        var auction = new Mock<Auction> { CallBase = true };
        var exitingAuction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.Vehicle = new Hatchback { Id = "3", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfDoors = 4 };
        exitingAuction.Object.Id = "1";
        exitingAuction.Object.Vehicle = new Hatchback { Id = "1", Manufacturer = "Honda", Year = 2020, Model = "Civic", StartingBid = 1500.00m, NumberOfDoors = 4 };

        _auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction> { exitingAuction.Object });

        var exception = Assert.Throws<CustomExceptions.AuctionSameIdException>(() => _service.StartAuction(auction.Object));
        Assert.Equal("An auction with the ID 1 already exists.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenVehicleNotFound()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "123";
        auction.Object.Vehicle = new Truck { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, LoadCapacity = 4 };

        _vehiclesRepositoryMock.Setup(repo => repo.GetVehicles()).Returns(new List<Vehicle?>());

        var exception = Assert.Throws<CustomExceptions.VehicleNotFoundException>(() => _service.StartAuction(auction.Object));
        Assert.Equal($"Vehicle with ID 1 not found.", exception.Message);
    }

    [Fact]
    public void StartAuction_ShouldStartAuction_WhenAuctionIsValid()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.Vehicle = new Suv { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfSeats = 4 };

        _vehiclesRepositoryMock.Setup(repo => repo.GetVehicles()).Returns(new List<Vehicle?> { auction.Object.Vehicle });

        _service.StartAuction(auction.Object);

        _auctionsRepositoryMock.Verify(repo => repo.StartAuction(auction.Object), Times.Once);
    }

    [Fact]
    public void GetAuctions_ShouldReturnAuctions_WhenAuctionsExist()
    {
        var auctions = new List<Auction> { new Mock<Auction>().Object };
        _auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(auctions);

        var result = _service.GetAuctions();

        Assert.Equal(auctions, result);
    }

    [Fact]
    public void GetAuctions_ShouldThrowException_WhenNoAuctionsFound()
    {
        _auctionsRepositoryMock.Setup(repo => repo.GetAuctions()).Returns(new List<Auction>());

        var exception = Assert.Throws<CustomExceptions.NoAuctionsFoundException>(() => _service.GetAuctions());

        Assert.Equal("No auctions found.", exception.Message);
    }

    [Fact]
    public void EndAuction_ShouldThrowException_WhenAuctionNotFound()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction>());

        var exception = Assert.Throws<CustomExceptions.AuctionNotFoundException>(() => _service.EndAuction(auction.Object));
        Assert.Equal($"Auction with ID 1 not found or isn't active.", exception.Message);
    }

    [Fact]
    public void EndAuction_ShouldEndAuction_WhenAuctionIsActive()
    {
        var auction = new Mock<Auction> { CallBase = true };
        auction.Object.Id = "1";
        auction.Object.IsActive = true;

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction.Object });

        _service.EndAuction(auction.Object);

        _auctionsRepositoryMock.Verify(repo => repo.EndAuction(auction.Object), Times.Once);
    }

    [Fact]
    public void EndAuction_ShouldSetIsActiveToFalse_WhenAuctionIsActive()
    {
        var auction = new Auction
        {
            Id = "1",
            IsActive = true,
            Vehicle = new Suv { Id = "1", Manufacturer = "Honda", Year = 2019, Model = "Civic", StartingBid = 1500.00m, NumberOfSeats = 4 },
            HighestBid = 1500.00m,
            HighestBidder = "1"
        };

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });
        _auctionsRepositoryMock.Setup(repo => repo.EndAuction(It.IsAny<Auction>())).Callback<Auction>(a => a.IsActive = false);

        _service.EndAuction(auction);

        _auctionsRepositoryMock.Verify(repo => repo.EndAuction(It.Is<Auction>(a => a.Id == "1")), Times.Once);
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

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });
        _auctionsRepositoryMock.Setup(repo => repo.PlaceBid(It.IsAny<Bid>())).Callback<Bid>(bid =>
        {
            auction.HighestBid = bid.Amount;
            auction.HighestBidder = bid.BidderId;
            auction.Bids.Add(bid);
        });

        _service.PlaceBid(newBid);

        _auctionsRepositoryMock.Verify(repo => repo.PlaceBid(It.Is<Bid>(a => a.Amount == 150 && a.BidderId == "2")), Times.Once);
        Assert.Equal(150, auction.HighestBid);
        Assert.Equal("2", auction.HighestBidder);
        Assert.Contains(newBid, auction.Bids);
    }

    [Theory]
    [InlineData("1", "Auction with ID 1 not found or isn't active.")]
    public void PlaceBid_ShouldThrowException_WhenAuctionNotFound(string auctionId, string expectedMessage)
    {
        var newBid = new Bid { AuctionId = auctionId, Amount = 150, BidderId = "2", Id = "2" };

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction>());

        var exception = Assert.Throws<CustomExceptions.AuctionNotFoundException>(() => _service.PlaceBid(newBid));
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(80, "The bid amount 80 is lower or equal to the current highest bid 100.")]
    public void PlaceBid_ShouldThrowException_WhenBidAmountTooLow(decimal bidAmount, string expectedMessage)
    {
        var auction = new Auction
        {
            Id = "1", HighestBid = 100, IsActive = true, HighestBidder = "2",
            Vehicle = new Sedan { Id = "1", Manufacturer = "Toyota", Year = 2020, Model = "Corolla", StartingBid = 50, NumberOfDoors = 4 },
            Bids = new List<Bid?> { new Bid { Id = "1", AuctionId = "1", Amount = 100, BidderId = "1" } }
        };
        var newBid = new Bid { Id = "3", BidderId = "55", AuctionId = "1", Amount = bidAmount };

        _auctionsRepositoryMock.Setup(repo => repo.GetActiveAuctions()).Returns(new List<Auction> { auction });

        var exception = Assert.Throws<CustomExceptions.BidAmountTooLowException>(() => _service.PlaceBid(newBid));
        Assert.Equal(expectedMessage, exception.Message);
    }
}