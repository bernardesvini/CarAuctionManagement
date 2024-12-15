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

    public List<Bidder?>? GetActivesBidders()
    {
        List<Bidder?>? bidders = _biddersRepository.GetActivesBidders();
        return bidders;
    }

    public List<Bidder?>? GetInactivesBidders()
    {
        List<Bidder?>? bidders = _biddersRepository.GetInactivesBidders();
        return bidders;
    }

    public Bidder? GetBidderById(Guid? id)
    {
        Bidder? bidder = _biddersRepository.GetBidderById(id);
        GetBidderByIdValidation(bidder, id);
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
        Bidder? bidderToRemove = _biddersRepository.GetBidderById(id);
        GetBidderByIdValidation(bidderToRemove, id);
        _biddersRepository.RemoveBidder(bidderToRemove?.GetId());
    }

    private void GetBidderByIdValidation(Bidder? bidder, Guid? receivedId)
    {
        if (bidder == null)
            throw new CustomExceptions.BidderNotFoundByIdException(receivedId);
    }

    private void BidderValidations(Bidder? bidder)
    {
        List<Bidder?>? existingBidders = _biddersRepository.GetBidders();
        if (existingBidders != null && existingBidders.Any(b => b?.GetEmail() == bidder?.GetEmail() && b?.GetName() == bidder?.GetName()))
            throw new CustomExceptions.BidderAlreadyExistsException(bidder?.GetEmail(), bidder?.GetName());
    }
    
    
}