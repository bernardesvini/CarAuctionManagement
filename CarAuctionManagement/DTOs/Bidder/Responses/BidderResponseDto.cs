namespace CarAuctionManagement.DTOs.Bidder.Responses;

public record BidderResponseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    public decimal? Amount { get; set; }
}