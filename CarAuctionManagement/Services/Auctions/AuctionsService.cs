using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Bidders;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Bidders;
using CarAuctionManagement.Repository.Vehicles;

namespace CarAuctionManagement.Services.Auctions;

public class AuctionsService : IAuctionsService
{
    private readonly IAuctionsRepository _auctionsRepository;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IBiddersRepository _biddersRepository;

    public AuctionsService(IAuctionsRepository auctionsRepository, IVehiclesRepository vehiclesRepository, IBiddersRepository biddersRepository)
    {
        _auctionsRepository = auctionsRepository;
        _vehiclesRepository = vehiclesRepository;
        _biddersRepository = biddersRepository;
    }

    public Auction? StartAuction(Auction? auction)
    {
        StartAuctionValidations(auction);
        Auction? response = _auctionsRepository.StartAuction(auction);
        return response;
    }

    public List<Auction?>? GetAuctions()
    {
        List<Auction?>? auctions = _auctionsRepository.GetAuctions();
        GetAuctionValidations(auctions);
        return auctions;
    }

    public Auction? GetAuctionById(Guid? id)
    {
        Auction? auction = _auctionsRepository.GetAuctionById(id);
        if (auction == null)
            throw new CustomExceptions.AuctionNotFoundException(id);
        return auction;
    }

    public void EndAuction(Guid? auctionId)
    {
        EndAuctionValidations(auctionId);
        _auctionsRepository.EndAuction(auctionId);
    }

    public Bid? PlaceBid(Bid? newBid)
    {
        PlaceBidValidations(newBid);
        Bid? addedBid = _auctionsRepository.PlaceBid(newBid);
        return addedBid;
    }

    private void PlaceBidValidations(Bid? newBid)
    {
        List<Auction?>? activeAuctions = GetActiveAuctions();
        if (activeAuctions.All(a => a?.GetId() != newBid?.GetAuctionId()))
            throw new CustomExceptions.AuctionNotFoundException(newBid?.GetAuctionId());
        Auction? auction = activeAuctions.First(a => a?.GetId() == newBid?.GetAuctionId());
        if (newBid?.GetAmount() <= auction?.GetHighestBid() || newBid?.GetAmount() <= auction?.GetVehicle()?.GetStartingBid())
            throw new CustomExceptions.BidAmountTooLowException(newBid.GetAmount(), auction.GetHighestBid());
    }

    public List<Auction?> GetActiveAuctions()
    {
        List<Auction?>? activeAuctions = _auctionsRepository.GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.Count == 0)
            throw new CustomExceptions.NoActiveAuctionsFoundException();
        return activeAuctions;
    }

    public List<Auction?> GetClosedAuctions()
    {
        List<Auction?>? closedAuctions = _auctionsRepository.GetClosedAuctions();
        if (closedAuctions == null || closedAuctions.Count == 0)
            throw new CustomExceptions.NoClosedAuctionsFoundException();
        return closedAuctions;
    }

    public Bidder? GetHighestBidder(Guid? auctionId)
    {
        Auction? auction = _auctionsRepository.GetHighestBidderId(auctionId);
        HighestBidderValidations(auctionId, auction);
        Bidder? highestBidder = _biddersRepository.GetBidderById(auction?.GetHighestBidder());
        return highestBidder;
    }

    private static void HighestBidderValidations(Guid? auctionId, Auction? auction)
    {
        if (auction == null)
            throw new CustomExceptions.AuctionNotFoundException(auctionId);
        if (auction?.GetHighestBidder() == null || auction?.GetHighestBidder() == Guid.Empty)
            throw new CustomExceptions.NoHighestBidderException(auctionId);
    }

    private void StartAuctionValidations(Auction? auction)
    {
        List<Auction?>? auctions = _auctionsRepository.GetAuctions();
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.GetVehicle()?.GetId() == auction?.GetVehicle()?.GetId() && actualAuction != null && actualAuction.GetIsActive()))
            throw new CustomExceptions.AuctionAlreadyActiveException(auction?.GetVehicle()?.GetId());
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.GetId() == auction?.GetId()))
            throw new CustomExceptions.AuctionSameIdException(auction?.GetId());
        List<Vehicle?> allVehicles = _vehiclesRepository.GetVehicles();
        if (allVehicles != null && allVehicles.All(vehicle => vehicle?.GetId() != auction?.GetVehicle()?.GetId()))
            throw new CustomExceptions.VehicleNotFoundException(auction?.GetVehicle()?.GetId());
    }

    private void EndAuctionValidations(Guid? auctionId)
    {
        List<Auction?>? activeAuctions = GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.All(a => a?.GetId() != auctionId))
            throw new CustomExceptions.AuctionNotFoundException(auctionId);
    }

    private static void GetAuctionValidations(List<Auction?>? auctions)
    {
        if (auctions?.Count == 0)
            throw new CustomExceptions.NoAuctionsFoundException();
    }
}