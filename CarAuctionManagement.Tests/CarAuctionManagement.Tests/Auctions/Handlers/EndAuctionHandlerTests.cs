using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.Handlers.Auctions.EndAuction;
using CarAuctionManagement.Services.Auctions;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Auctions.Handlers;

public class EndAuctionHandlerTests
{
    private readonly Mock<IAuctionsService> _auctionsServiceMock;
    private readonly EndAuctionHandler _handler;

    public EndAuctionHandlerTests()
    {
        _auctionsServiceMock = new Mock<IAuctionsService>();
        _handler = new EndAuctionHandler(_auctionsServiceMock.Object);
    }

    [Fact]
    public void EndAuction_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        var request = new EndAuctionRequestDto { AuctionId = Guid.Empty };

        Assert.Throws<ValidationException>(() => _handler.EndAuction(request));
    }

    [Fact]
    public void EndAuction_ShouldCallService_WhenRequestIsValid()
    {
        var request = new EndAuctionRequestDto { AuctionId = Guid.NewGuid() };

        _handler.EndAuction(request);

        _auctionsServiceMock.Verify(a => a.EndAuction(request.AuctionId), Times.Once);
    }
}