using CarAuctionManagement.DTOs.Auctions.Responses;
using CarAuctionManagement.DTOs.Bidder.Responses;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Services.Auctions;

namespace CarAuctionManagement.Handlers.Auctions.GetAuctions;

public class GetAuctionHandler : IGetAuctionHandler
{
    private readonly IAuctionsService _auctionsService;
    
    public GetAuctionHandler(IAuctionsService auctionsService)
    {
        _auctionsService = auctionsService;
    }   
    
    public List<GetAuctionResponseDto?>? GetAuctions()
    {
        List<GetAuctionResponseDto?>? auctions = _auctionsService.GetAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        return auctions;
    }

    public GetAuctionResponseDto? GetAuctionById(Guid? auctionId)
    {
        Auction? auction = _auctionsService.GetAuctionById(auctionId);
        GetAuctionResponseDto? response = auction?.ToGetResponseDto();
        return response;
    }

    public List<GetAuctionResponseDto?>? GetActiveAuctions()
    {
        List<GetAuctionResponseDto?>? auctions = _auctionsService.GetActiveAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        return auctions;
    }
    
    public List<GetAuctionResponseDto?>? GetClosedAuctions()
    {
        List<GetAuctionResponseDto?>? auctions = _auctionsService.GetClosedAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        return auctions;
    }

    public BidderResponseDto? GetHighestBidder(Guid? auctionId)
    {
        Bidder? highestBidder = _auctionsService.GetHighestBidder(auctionId);
        BidderResponseDto? response = highestBidder?.ToResponseDto();
        return response;
    }
}