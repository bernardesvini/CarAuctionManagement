using CarAuctionManagement.Handlers.Bidders.RemoveBidders;
using CarAuctionManagement.Services.Bidders;
using Moq;

namespace CarAuctionManagement.Tests.Bidders.Handlers
{
    public class RemoveBidderHandlerTests
    {
        private readonly Mock<IBiddersService> _biddersServiceMock;
        private readonly RemoveBidderHandler _handler;

        public RemoveBidderHandlerTests()
        {
            _biddersServiceMock = new Mock<IBiddersService>();
            _handler = new RemoveBidderHandler(_biddersServiceMock.Object);
        }

        [Fact]
        public void RemoveBidder_ShouldCallServiceRemoveBidder_WhenBidderExists()
        {
            var bidderId = Guid.NewGuid();

            _handler.RemoveBidder(bidderId);

            _biddersServiceMock.Verify(s => s.RemoveBidder(bidderId), Times.Once);
        }
    }
}