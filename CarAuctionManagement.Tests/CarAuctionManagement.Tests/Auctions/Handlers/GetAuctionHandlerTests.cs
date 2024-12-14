using CarAuctionManagement.Handlers.Auctions.GetAuctions;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Services.Auctions;
using Moq;

namespace CarAuctionManagement.Tests.Auctions.Handlers;

public class GetAuctionHandlerTests
{
    private readonly Mock<IAuctionsService> _auctionsServiceMock;
    private readonly GetAuctionHandler _handler;

    public GetAuctionHandlerTests()
    {
        _auctionsServiceMock = new Mock<IAuctionsService>();
        _handler = new GetAuctionHandler(_auctionsServiceMock.Object);
    }

    [Fact]
    public void GetAuctions_ShouldReturnListOfAuctions()
    {
        var auctions = new List<Auction> { new Auction { Id = Guid.NewGuid() } };

        _auctionsServiceMock.Setup(a => a.GetAuctions()).Returns(auctions);

        var result = _handler.GetAuctions();

        Assert.NotNull(result);
        Assert.Equal(auctions, result);
    }

    [Fact]
    public void GetActiveAuctions_ShouldReturnListOfActiveAuctions()
    {
        var auctions = new List<Auction> { new Auction { Id = Guid.NewGuid(), IsActive = true } };

        _auctionsServiceMock.Setup(a => a.GetActiveAuctions()).Returns(auctions);

        var result = _handler.GetActiveAuctions();

        Assert.NotNull(result);
        Assert.Equal(auctions, result);
    }

    [Fact]
    public void GetClosedAuctions_ShouldReturnListOfClosedAuctions()
    {
        var auctions = new List<Auction> { new Auction { Id = Guid.NewGuid(), IsActive = false } };

        _auctionsServiceMock.Setup(a => a.GetClosedAuctions()).Returns(auctions);

        var result = _handler.GetClosedAuctions();

        Assert.NotNull(result);
        Assert.Equal(auctions, result);
    }
}