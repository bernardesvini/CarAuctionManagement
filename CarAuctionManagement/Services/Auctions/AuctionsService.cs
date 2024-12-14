using CarAuctionManagement.ErrorHandling;
using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Vehicles;

namespace CarAuctionManagement.Services.Auctions;

public class AuctionsService : IAuctionsService
{
    private readonly IAuctionsRepository _auctionsRepository;
    private readonly IVehiclesRepository _vehiclesRepository;

    public AuctionsService(IAuctionsRepository auctionsRepository, IVehiclesRepository vehiclesRepository)
    {
        _auctionsRepository = auctionsRepository;
        _vehiclesRepository = vehiclesRepository;
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
        if (activeAuctions == null || activeAuctions.All(a => a?.GetId() != newBid?.GetAuctionId()))
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

    private void StartAuctionValidations(Auction? auction)
    {
        List<Auction?>? auctions = _auctionsRepository.GetAuctions();
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.GetVehicle()?.GetId() == auction?.GetVehicle()?.GetId() && actualAuction.GetIsActive()))
            throw new CustomExceptions.AuctionAlreadyActiveException(auction?.GetVehicle()?.GetId());
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.GetId() == auction?.GetId()))
            throw new CustomExceptions.AuctionSameIdException(auction?.GetId());
        List<Vehicle?> allVehicles = _vehiclesRepository.GetVehicles();
        if (allVehicles.All(vehicle => vehicle?.GetId() != auction?.GetVehicle()?.GetId()))
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