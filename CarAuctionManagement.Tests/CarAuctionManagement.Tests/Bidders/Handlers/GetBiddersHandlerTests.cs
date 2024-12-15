using CarAuctionManagement.Handlers.Bidders.GetBidders;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Services.Bidders;
using Moq;

namespace CarAuctionManagement.Tests.Bidders.Handlers
{
    public class GetBiddersHandlerTests
    {
        private readonly Mock<IBiddersService> _biddersServiceMock;
        private readonly GetBiddersHandler _handler;

        public GetBiddersHandlerTests()
        {
            _biddersServiceMock = new Mock<IBiddersService>();
            _handler = new GetBiddersHandler(_biddersServiceMock.Object);
        }

        [Fact]
        public void GetBidders_ShouldReturnPaginatedListOfBidders()
        {
            var bidders = new List<Bidder?> { new Bidder(Guid.NewGuid(), "John Doe", "john.doe@example.com") };
            _biddersServiceMock.Setup(s => s.GetBidders()).Returns(bidders);

            var result = _handler.GetBidders(1, 10);

            Assert.NotNull(result);
            Assert.Equal(bidders.Count, result?.TotalCount);
            Assert.Equal(1, result?.Page);
            Assert.Equal(10, result?.PageSize);
            Assert.Equal(bidders.Count, result?.BiddersList?.Count);
        }

        [Fact]
        public void GetActivesBidders_ShouldReturnPaginatedListOfActiveBidders()
        {
            var bidders = new List<Bidder?> { new Bidder(Guid.NewGuid(), "John Doe", "john.doe@example.com") };
            _biddersServiceMock.Setup(s => s.GetActivesBidders()).Returns(bidders);

            var result = _handler.GetActivesBidders(1, 10);

            Assert.NotNull(result);
            Assert.Equal(bidders.Count, result?.TotalCount);
            Assert.Equal(1, result?.Page);
            Assert.Equal(10, result?.PageSize);
            Assert.Equal(bidders.Count, result?.BiddersList?.Count);
        }

        [Fact]
        public void GetInactivesBidders_ShouldReturnPaginatedListOfInactiveBidders()
        {
            var bidders = new List<Bidder?> { new Bidder(Guid.NewGuid(), "John Doe", "john.doe@example.com") };
            _biddersServiceMock.Setup(s => s.GetInactivesBidders()).Returns(bidders);

            var result = _handler.GetInactivesBidders(1, 10);

            Assert.NotNull(result);
            Assert.Equal(bidders.Count, result?.TotalCount);
            Assert.Equal(1, result?.Page);
            Assert.Equal(10, result?.PageSize);
            Assert.Equal(bidders.Count, result?.BiddersList?.Count);
        }

        [Fact]
        public void GetBidderById_ShouldReturnBidder_WhenBidderExists()
        {
            var bidderId = Guid.NewGuid();
            var bidder = new Bidder(bidderId, "John Doe", "john.doe@example.com");
            _biddersServiceMock.Setup(s => s.GetBidderById(bidderId)).Returns(bidder);

            var result = _handler.GetBidderById(bidderId);

            Assert.NotNull(result);
            Assert.Equal(bidderId, result?.Id);
        }

        [Fact]
        public void GetBidderById_ShouldReturnNull_WhenBidderDoesNotExist()
        {
            var bidderId = Guid.NewGuid();
            _biddersServiceMock.Setup(s => s.GetBidderById(bidderId)).Returns((Bidder?)null);

            var result = _handler.GetBidderById(bidderId);

            Assert.Null(result);
        }
    }
}