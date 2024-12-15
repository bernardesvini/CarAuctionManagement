using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Repository.Bidders;

namespace CarAuctionManagement.Services.Bidders;

public class BiddersService : IBiddersService
{
    private readonly IBiddersRepository _biddersRepository;
    
    public BiddersService(IBiddersRepository biddersRepository)
    {
        _biddersRepository = biddersRepository;
    }
    
    public Bidder? CreateBidder(Bidder? bidder)
    {
        
        BidderValidations(bidder);
        Bidder? response = _biddersRepository.AddBidder(bidder);
        return response;
    }
    
    public List<Bidder?>? GetBidders()
    {
        List<Bidder?>? bidders = _biddersRepository.GetBidders();
        return bidders;
    }

    public Bidder GetBidderById(Guid? id)
    {
        Bidder bidder = GetBidderByIdValidation(id);
        return bidder;
    }
    
    public Bidder? UpdateBidder(Bidder? bidder)
    {
        BidderValidations(bidder);
        GetBidderById(bidder?.GetId());
        Bidder? response = _biddersRepository.UpdateBidder(bidder);
        return response;
    }
    
    public void RemoveBidder(Guid? id)
    {
        Bidder bidderToRemove = GetBidderByIdValidation(id);
        _biddersRepository.RemoveBidder(bidderToRemove.GetId());
    }

    private Bidder GetBidderByIdValidation(Guid? id)
    {
        List<Bidder?>? existingBidders = _biddersRepository.GetBidders();
        Bidder? bidder = existingBidders?.FirstOrDefault(b => b?.GetId() == id);
        if (bidder == null)
            throw new CustomExceptions.BidderNotFoundByIdException(id);
        return bidder;
    }

    private void BidderValidations(Bidder? bidder)
    {
        List<Bidder?>? existingBidders = _biddersRepository.GetBidders();
        if (existingBidders != null && existingBidders.Any(b => b?.GetEmail() == bidder?.GetEmail() && b?.GetName() == bidder?.GetName()))
            throw new CustomExceptions.BidderAlreadyExistsException(bidder?.GetEmail(), bidder?.GetName());
    }
    
    
}