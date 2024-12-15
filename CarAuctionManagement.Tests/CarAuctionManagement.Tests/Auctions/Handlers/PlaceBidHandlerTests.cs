using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.Handlers.Auctions.PlaceBid;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Bidders;
using FluentValidation;
using Moq;

namespace CarAuctionManagement.Tests.Auctions.Handlers;

public class PlaceBidHandlerTests
{
    private readonly Mock<IAuctionsService> _auctionsServiceMock;
    private readonly PlaceBidHandler _handler;
    
    public PlaceBidHandlerTests()
    {
        _auctionsServiceMock = new Mock<IAuctionsService>();
        _handler = new PlaceBidHandler(_auctionsServiceMock.Object, new Mock<IBiddersService>().Object);
    }
    
    [Fact]
    public void PlaceBid_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        var request = new PlaceBidRequestDto { BidderId = null, AuctionId = Guid.Empty, Amount = null };
    
        Assert.Throws<ValidationException>(() => _handler.PlaceBid(request));
    }
    
    [Fact]
    public void PlaceBid_ShouldReturnResponse_WhenRequestIsValid()
    {
        var bid = new Bid(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1000.00m);
        var request = new PlaceBidRequestDto { BidderId = bid.GetId(), AuctionId = bid.GetAuctionId(), Amount = 1000.00m };
    
        _auctionsServiceMock.Setup(a => a.PlaceBid(It.IsAny<Bid>())).Returns(bid);
    
        var response = _handler.PlaceBid(request);
    
        Assert.NotNull(response);
        Assert.Equal(bid.GetId(), response.Id);
        Assert.Equal(bid.GetAuctionId(), response.AuctionId);
        Assert.Equal(bid.GetAmount(), response.Amount);
        Assert.Equal(bid.GetBidderId(), response.BidderId);
    }
}