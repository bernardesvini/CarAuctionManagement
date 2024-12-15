using CarAuctionManagement.DTOs.Bidder.Requests;
using CarAuctionManagement.Handlers.Bidders.CreateBidder;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Services.Bidders;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Bidders.Handlers
{
    public class CreateBidderHandlerTests
    {
        private readonly Mock<IBiddersService> _biddersServiceMock;
        private readonly CreateBidderHandler _handler;

        public CreateBidderHandlerTests()
        {
            _biddersServiceMock = new Mock<IBiddersService>();
            _handler = new CreateBidderHandler(_biddersServiceMock.Object);
        }

        [Fact]
        public void CreateBidder_ShouldReturnBidderResponse_WhenBidderIsValid()
        {
            var request = new CreateBidderRequestDto { Name = "John Doe", Email = "john.doe@example.com" };
            var bidder = new Bidder(Guid.NewGuid(), request.Name, request.Email);
            _biddersServiceMock.Setup(s => s.CreateBidder(It.IsAny<Bidder>())).Returns(bidder);

            var result = _handler.CreateBidder(request);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result?.Name);
            Assert.Equal(request.Email, result?.Email);
        }

        [Fact]
        public void CreateBidder_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var request = new CreateBidderRequestDto { Name = "", Email = "invalid-email" };

            Assert.Throws<ValidationException>(() => _handler.CreateBidder(request));
        }

        [Fact]
        public void UpdateBidder_ShouldReturnUpdatedBidderResponse_WhenBidderIsValid()
        {
            var bidderId = Guid.NewGuid();
            var request = new UpdateBidderRequestDto { Name = "Jane Doe", Email = "jane.doe@example.com" };
            var existingBidder = new Bidder(bidderId, "John Doe", "john.doe@example.com");
            var updatedBidder = new Bidder(bidderId, request.Name, request.Email);
            _biddersServiceMock.Setup(s => s.GetBidderById(bidderId)).Returns(existingBidder);
            _biddersServiceMock.Setup(s => s.UpdateBidder(It.IsAny<Bidder>())).Returns(updatedBidder);

            var result = _handler.UpdateBidder(request, bidderId);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result?.Name);
            Assert.Equal(request.Email, result?.Email);
        }

        [Fact]
        public void UpdateBidder_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var bidderId = Guid.NewGuid();
            var request = new UpdateBidderRequestDto { Name = "", Email = "invalid-email" };

            Assert.Throws<ValidationException>(() => _handler.UpdateBidder(request, bidderId));
        }
    }
}