using CarAuctionManagement.Handlers.Auctions.GetAuctions;
using CarAuctionManagement.Handlers.Auctions.PlaceBid;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Bidders;
using Moq;

namespace CarAuctionManagement.Tests.Auctions.Handlers;

public class GetAuctionHandlerTests
{
    private readonly Mock<IAuctionsService> _auctionsServiceMock;
    private readonly GetAuctionHandler _handler;
    private readonly Mock<IBiddersService> _placeBidServiceMock;

    public GetAuctionHandlerTests()
    {
        _placeBidServiceMock = new Mock<IBiddersService>();
        _auctionsServiceMock = new Mock<IAuctionsService>();
        _handler = new GetAuctionHandler(_auctionsServiceMock.Object);
    }

    [Fact]
public void GetAuctions_ShouldReturnPaginatedListOfAuctions()
{
    var auctions = new List<Auction> { new Auction(Guid.NewGuid(), null, false, null) };
    _auctionsServiceMock.Setup(a => a.GetAuctions()).Returns(auctions);

    var result = _handler.GetAuctions(1, 10);

    Assert.NotNull(result);
    Assert.Equal(auctions.Count, result.TotalCount);
    Assert.Equal(1, result.Page);
    Assert.Equal(10, result.PageSize);
    Assert.Equal(auctions.Count, result.AuctionsList?.Count);
}

[Fact]
public void GetAuctionById_ShouldReturnAuction_WhenAuctionExists()
{
    var auctionId = Guid.NewGuid();
    var auction = new Auction(auctionId, null, false, null);
    _auctionsServiceMock.Setup(a => a.GetAuctionById(auctionId)).Returns(auction);

    var result = _handler.GetAuctionById(auctionId);

    Assert.NotNull(result);
    Assert.Equal(auctionId, result.Id);
}

[Fact]
public void GetAuctionById_ShouldReturnNull_WhenAuctionDoesNotExist()
{
    var auctionId = Guid.NewGuid();
    _auctionsServiceMock.Setup(a => a.GetAuctionById(auctionId)).Returns((Auction)null);

    var result = _handler.GetAuctionById(auctionId);

    Assert.Null(result);
}

[Fact]
public void GetActiveAuctions_ShouldReturnPaginatedListOfActiveAuctions()
{
    var auctions = new List<Auction> { new Auction(Guid.NewGuid(), null, true, null) };
    _auctionsServiceMock.Setup(a => a.GetActiveAuctions()).Returns(auctions);

    var result = _handler.GetActiveAuctions(1, 10);

    Assert.NotNull(result);
    Assert.Equal(auctions.Count, result.TotalCount);
    Assert.Equal(1, result.Page);
    Assert.Equal(10, result.PageSize);
    Assert.Equal(auctions.Count, result.AuctionsList?.Count);
}

[Fact]
public void GetClosedAuctions_ShouldReturnPaginatedListOfClosedAuctions()
{
    var auctions = new List<Auction> { new Auction(Guid.NewGuid(), null, false, null) };
    _auctionsServiceMock.Setup(a => a.GetClosedAuctions()).Returns(auctions);

    var result = _handler.GetClosedAuctions(1, 10);

    Assert.NotNull(result);
    Assert.Equal(auctions.Count, result.TotalCount);
    Assert.Equal(1, result.Page);
    Assert.Equal(10, result.PageSize);
    Assert.Equal(auctions.Count, result.AuctionsList?.Count);
}

[Fact]
public void GetHighestBidder_ShouldReturnHighestBidderWithAmount_WhenBidderExists()
{
    var auctionId = Guid.NewGuid();
    var highestBidder = new Bidder(Guid.NewGuid(), "John Doe", "sdas@sadas");
    var vehicle = new Vehicle( Guid.NewGuid(), "Ford", "Focus", 2010, 10000);
    var auction = new Auction(auctionId, vehicle, false, null, 10000, Guid.NewGuid());
    _auctionsServiceMock.Setup(a => a.GetHighestBidder(auctionId)).Returns(highestBidder);
    _auctionsServiceMock.Setup(a => a.GetAuctionById(auctionId)).Returns(auction);
    _placeBidServiceMock.Setup(bid => bid.GetBidderById(highestBidder.GetId())).Returns(highestBidder);

    var result = _handler.GetHighestBidder(auctionId);

    Assert.NotNull(result);
    Assert.Equal(highestBidder.GetId(), result.Id);
    Assert.Equal(10000, result.Amount);
}

[Fact]
public void GetHighestBidder_ShouldReturnNull_WhenBidderDoesNotExist()
{
    var auctionId = Guid.NewGuid();
    _auctionsServiceMock.Setup(a => a.GetHighestBidder(auctionId)).Returns((Bidder)null);

    var result = _handler.GetHighestBidder(auctionId);

    Assert.Null(result);
}
}