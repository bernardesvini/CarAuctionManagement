using FluentValidation;

namespace CarAuctionManagement.DTOs.Bidder.Requests;

public class UpdateBidderRequestDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}