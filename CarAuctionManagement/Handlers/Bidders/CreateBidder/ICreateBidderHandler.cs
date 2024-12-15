using CarAuctionManagement.DTOs.Bidder.Requests;
using CarAuctionManagement.DTOs.Bidder.Responses;

namespace CarAuctionManagement.Handlers.Bidders.CreateBidder;

public interface ICreateBidderHandler
{
    BidderResponseDto? CreateBidder(CreateBidderRequestDto bidder);
    BidderResponseDto? UpdateBidder(UpdateBidderRequestDto bidder, Guid id);
}