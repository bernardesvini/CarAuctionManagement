using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Bidders;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Auctions;
using Moq;

namespace CarAuctionManagement.Tests.Auctions
{
    public class AuctionsServiceTests
    {
        private readonly Mock<IAuctionsRepository> _mockAuctionsRepository;
        private readonly Mock<IBiddersRepository> _mockBiddersRepository;
        private readonly AuctionsService _auctionsService;

        public AuctionsServiceTests()
        {
            _mockAuctionsRepository = new Mock<IAuctionsRepository>();
            Mock<IVehiclesRepository> mockVehiclesRepository = new();
            _mockBiddersRepository = new Mock<IBiddersRepository>();
            _auctionsService = new AuctionsService(
                _mockAuctionsRepository.Object,
                mockVehiclesRepository.Object,
                _mockBiddersRepository.Object);
        }

        [Fact]
        public void StartAuction_ValidAuction_ExecutesSuccessfully()
        {
            var auction = new Auction(Guid.NewGuid(), new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 1000.00m), true, new List<Bid?>(), 1000, Guid.NewGuid());
            _mockAuctionsRepository.Setup(r => r.StartAuction(It.IsAny<Auction>())).Returns(auction);

            var result = _auctionsService.StartAuction(auction);

            Assert.NotNull(result);
            _mockAuctionsRepository.Verify(r => r.StartAuction(It.IsAny<Auction>()), Times.Once);
        }

        [Fact]
        public void StartAuction_AuctionAlreadyActive_ThrowsAuctionAlreadyActiveException()
        {
            var auction = new Auction(Guid.NewGuid(), new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 1000.00m), false, new List<Bid?>(), 0, Guid.NewGuid());
            _mockAuctionsRepository.Setup(r => r.GetAuctions()).Returns(new List<Auction?>
            {
                new Auction(auction.GetId(), auction.GetVehicle(), true, new List<Bid?>(), 0, Guid.NewGuid())
            });

            var exception = Assert.Throws<CustomExceptions.AuctionAlreadyActiveException>(() => _auctionsService.StartAuction(auction));
            Assert.Equal($"An auction is already active for vehicle ID {auction.GetVehicle()?.GetId()}.", exception.Message);
        }

        [Fact]
        public void GetAuctionById_AuctionNotFound_ThrowsAuctionNotFoundException()
        {
            var auctionId = Guid.NewGuid();
            _mockAuctionsRepository.Setup(r => r.GetAuctionById(auctionId)).Returns((Auction?)null);

            var exception = Assert.Throws<CustomExceptions.AuctionNotFoundException>(() => _auctionsService.GetAuctionById(auctionId));
            Assert.Equal($"Auction with ID {auctionId} not found or isn't active.", exception.Message);
        }

        [Fact]
        public void EndAuction_ValidAuction_ExecutesSuccessfully()
        {
            var auctionId = Guid.NewGuid();
            _mockAuctionsRepository.Setup(r => r.GetActiveAuctions()).Returns(new List<Auction?>
                { new Auction(auctionId, new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 1000.00m), true, new List<Bid?>(), 0, Guid.NewGuid()) });

            _auctionsService.EndAuction(auctionId);

            _mockAuctionsRepository.Verify(r => r.EndAuction(auctionId), Times.Once);
        }

        [Fact]
        public void PlaceBid_ValidBid_ExecutesSuccessfully()
        {
            var auctionId = Guid.NewGuid();
            var newBid = new Bid(Guid.NewGuid(), Guid.NewGuid(), auctionId, 1000);
            var auction = new Auction(auctionId, new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 900.00m), true, new List<Bid?>(), 500, Guid.NewGuid());
            _mockAuctionsRepository.Setup(r => r.GetActiveAuctions()).Returns(new List<Auction?> { auction });
            _mockAuctionsRepository.Setup(r => r.PlaceBid(It.IsAny<Bid>())).Returns(newBid);

            var result = _auctionsService.PlaceBid(newBid);

            Assert.NotNull(result);
            _mockAuctionsRepository.Verify(r => r.PlaceBid(It.IsAny<Bid>()), Times.Once);
        }

        [Fact]
        public void GetActiveAuctions_NoActiveAuctions_ThrowsNoActiveAuctionsFoundException()
        {
            _mockAuctionsRepository.Setup(r => r.GetActiveAuctions()).Returns(new List<Auction?>());

            Assert.Throws<CustomExceptions.NoActiveAuctionsFoundException>(() => _auctionsService.GetActiveAuctions());
        }

        [Fact]
        public void GetHighestBidder_ValidAuction_ReturnsHighestBidder()
        {
            var auctionId = Guid.NewGuid();
            var auction = new Auction(auctionId, new Vehicle(Guid.NewGuid(), "Model", "Make", 2020, 1000.00m), true, new List<Bid?>(), 0, Guid.NewGuid());
            _mockAuctionsRepository.Setup(r => r.GetHighestBidderId(auctionId)).Returns(auction);
            _mockBiddersRepository.Setup(r => r.GetBidderById(auction.GetHighestBidder())).Returns(new Bidder(auction.GetHighestBidder(), "John Doe", "email@example.com"));

            var result = _auctionsService.GetHighestBidder(auctionId);

            Assert.NotNull(result);
        }
    }
}