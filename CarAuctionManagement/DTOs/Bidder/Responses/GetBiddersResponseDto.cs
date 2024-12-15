namespace CarAuctionManagement.DTOs.Bidder.Responses;

public class GetBiddersResponseDto
{
    public int? TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<BidderResponseDto?>? BiddersList { get; set; }
}