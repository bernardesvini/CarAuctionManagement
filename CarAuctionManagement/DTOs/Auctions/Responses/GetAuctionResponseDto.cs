namespace CarAuctionManagement.DTOs.Auctions.Responses;

public class GetAuctionResponseDto
{
    public int? TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<AuctionResponseDto?>? AuctionsList { get; set; }
}

