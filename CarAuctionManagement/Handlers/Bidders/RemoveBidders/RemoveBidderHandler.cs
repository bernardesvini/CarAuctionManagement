using CarAuctionManagement.Services.Bidders;

namespace CarAuctionManagement.Handlers.Bidders.RemoveBidders;

public class RemoveBidderHandler : IRemoveBidderHandler
{
    private readonly IBiddersService _biddersService;
    
    public RemoveBidderHandler(IBiddersService biddersService)
    {
        _biddersService = biddersService;
    }
    
    public void RemoveBidder(Guid? bidderId)
    {
        _biddersService.RemoveBidder(bidderId);
    }
}