using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Repository.Bidders;
using CarAuctionManagement.Services.Bidders;
using Moq;

namespace CarAuctionManagement.Tests.Bidders
{
    public class BiddersServicesTests
    {
        private readonly Mock<IBiddersRepository> _biddersRepositoryMock;
        private readonly BiddersService _biddersService;

        public BiddersServicesTests()
        {
            _biddersRepositoryMock = new Mock<IBiddersRepository>();
            _biddersService = new BiddersService(_biddersRepositoryMock.Object);
        }

        [Fact]
        public void CreateBidder_ShouldAddBidder_WhenValid()
        {
            var bidder = new Bidder(Guid.NewGuid(), "John Doe", "john.doe@example.com");
            _biddersRepositoryMock.Setup(r => r.AddBidder(bidder)).Returns(bidder);

            var result = _biddersService.CreateBidder(bidder);

            _biddersRepositoryMock.Verify(r => r.AddBidder(bidder), Times.Once);
            Assert.Equal(bidder, result);
        }

        [Fact]
        public void GetBidderById_ShouldReturnBidder_WhenExists()
        {
            var bidderId = Guid.NewGuid();
            var bidder = new Bidder(bidderId, "John Doe", "john.doe@example.com");
            _biddersRepositoryMock.Setup(r => r.GetBidderById(bidderId)).Returns(bidder);

            var result = _biddersService.GetBidderById(bidderId);

            Assert.Equal(bidder, result);
        }

        [Fact]
        public void UpdateBidder_ShouldUpdateBidder_WhenValid()
        {
            var bidder = new Bidder(Guid.NewGuid(), "John Doe", "john.doe@example.com");
            _biddersRepositoryMock.Setup(r => r.GetBidderById(bidder.GetId())).Returns(bidder);
            _biddersRepositoryMock.Setup(r => r.UpdateBidder(bidder)).Returns(bidder);

            var result = _biddersService.UpdateBidder(bidder);

            _biddersRepositoryMock.Verify(r => r.UpdateBidder(bidder), Times.Once);
            Assert.Equal(bidder, result);
        }

        [Fact]
        public void RemoveBidder_ShouldRemoveBidder_WhenExists()
        {
            var bidderId = Guid.NewGuid();
            var bidder = new Bidder(bidderId, "John Doe", "john.doe@example.com");
            _biddersRepositoryMock.Setup(r => r.GetBidderById(bidderId)).Returns(bidder);

            _biddersService.RemoveBidder(bidderId);

            _biddersRepositoryMock.Verify(r => r.RemoveBidder(bidderId), Times.Once);
        }
    }
}