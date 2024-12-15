using CarAuctionManagement.DTOs.Bidder.Responses;
using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Services.Bidders;

namespace CarAuctionManagement.Handlers.Bidders.GetBidders;

public class GetBiddersHandler : IGetBiddersHandler
{
    private readonly IBiddersService _biddersService;
    
    public GetBiddersHandler(IBiddersService biddersService)
    {
        _biddersService = biddersService;
    }
    
    public GetBiddersResponseDto? GetBidders(int page, int pageSize)
    {
        List<Bidder?>? bidders = _biddersService.GetBidders();
        
        List<BidderResponseDto?>? allBidders = bidders?.Select(bidder => bidder?.ToResponseDto()).ToList();
        if(allBidders?.Count == 0)
            throw new CustomExceptions.BidderNotFoundException();
        
        var paginatedResponse = allBidders?
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)             
            .ToList();
       
        var response = new GetBiddersResponseDto()
        {
            TotalCount = allBidders?.Count,
            Page = page,
            PageSize = pageSize,
            BiddersList = paginatedResponse
        };
        
        return response;
    }
    
    public BidderResponseDto? GetBidderById(Guid? bidderId)
    {
        Bidder? bidder = _biddersService.GetBidderById(bidderId);
        BidderResponseDto? response = bidder?.ToResponseDto();
        return response;
    }
}
