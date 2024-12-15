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
    
    public GetAuctionResponseDto? GetAuctions(int page, int pageSize)
    {
        List<AuctionResponseDto?>? auctions = _auctionsService.GetAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        var response = CreatePaginatedResponse(page, pageSize, auctions);

        return response;
    }

    public AuctionResponseDto? GetAuctionById(Guid? auctionId)
    {
        Auction? auction = _auctionsService.GetAuctionById(auctionId);
        AuctionResponseDto? response = auction?.ToGetResponseDto();
        return response;
    }

    public GetAuctionResponseDto? GetActiveAuctions(int page, int pageSize)
    {
        List<AuctionResponseDto?>? auctions = _auctionsService.GetActiveAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        var response = CreatePaginatedResponse(page, pageSize, auctions);
        return response;
    }
    
    public GetAuctionResponseDto? GetClosedAuctions(int page, int pageSize)
    {
        List<AuctionResponseDto?>? auctions = _auctionsService.GetClosedAuctions()?.Select(auction => auction?.ToGetResponseDto()).ToList();
        var response = CreatePaginatedResponse(page, pageSize, auctions);
        return response;
    }

    public BidderResponseDto? GetHighestBidder(Guid? auctionId)
    {
        Bidder? highestBidder = _auctionsService.GetHighestBidder(auctionId);
        BidderResponseDto? response = highestBidder?.ToResponseDto();
        return response;
    }
    
    private static GetAuctionResponseDto CreatePaginatedResponse(int page, int pageSize, List<AuctionResponseDto?>? auctions)
    {
        var paginatedResponse = auctions?
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)             
            .ToList();

        var response = new GetAuctionResponseDto
        {
            TotalCount = auctions?.Count,
            Page = page,
            PageSize = pageSize,
            AuctionsList = paginatedResponse
        };
        return response;
    }
}