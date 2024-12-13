using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Services.Auctions;

namespace CarAuctionManagement.Handlers.Auctions.GetAuctions;

public class GetAuctionHandler : IGetAuctionHandler
{
    private readonly IAuctionsService _auctionsService;
    
    public GetAuctionHandler(IAuctionsService auctionsService)
    {
        _auctionsService = auctionsService;
    }
    
    public List<Auction> GetAuctions()
    {
        return _auctionsService.GetAuctions();
    }
    
    public List<Auction>? GetActiveAuctions()
    {
        return _auctionsService.GetActiveAuctions();
    }
    
    public List<Auction>? GetClosedAuctions()
    {
        return _auctionsService.GetClosedAuctions();
    }
}