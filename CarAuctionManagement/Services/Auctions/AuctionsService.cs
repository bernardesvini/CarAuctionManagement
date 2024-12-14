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
        auction?.Validate();
        StartAuctionValidations(auction);
        return _auctionsRepository.StartAuction(auction);
    }

    public List<Auction?>? GetAuctions()
    {
        List<Auction?>? auctions = GetAuctionValidations();
        return auctions;
    }

    public void EndAuction(Guid? auctionId)
    {
        EndAuctionValidations(auctionId);
        _auctionsRepository.EndAuction(auctionId);
    }

    public Bid? PlaceBid(Bid? newBid)
    {
        newBid?.Validate();
        PlaceBidValidations(newBid);
        Bid? addedBid = _auctionsRepository.PlaceBid(newBid);
        return addedBid;
    }

    private void PlaceBidValidations(Bid? newBid)
    {
        List<Auction?>? activeAuctions = GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.All(a => a?.Id != newBid?.AuctionId))
            throw new CustomExceptions.AuctionNotFoundException(newBid?.AuctionId);
        Auction? auction = activeAuctions.First(a => a?.Id == newBid?.AuctionId);
        if (newBid?.Amount <= auction?.HighestBid || newBid?.Amount <= auction?.Vehicle?.StartingBid)
            throw new CustomExceptions.BidAmountTooLowException(newBid.Amount, auction.HighestBid);
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
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.Vehicle?.Id == auction?.Vehicle?.Id))
            throw new CustomExceptions.AuctionAlreadyActiveException(auction?.Vehicle?.Id);
        if (auctions != null && auctions.Any(actualAuction => actualAuction?.Id == auction?.Id))
            throw new CustomExceptions.AuctionSameIdException(auction?.Id);
        List<Vehicle?> allVehicles = _vehiclesRepository.GetVehicles();
        if (allVehicles.All(vehicle => vehicle?.Id != auction?.Vehicle?.Id))
            throw new CustomExceptions.VehicleNotFoundException(auction?.Vehicle?.Id);
    }

    private void EndAuctionValidations(Guid? auctionId)
    {
        List<Auction?>? activeAuctions = GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.All(a => a?.Id != auctionId))
            throw new CustomExceptions.AuctionNotFoundException(auctionId);
    }

    private List<Auction?>? GetAuctionValidations()
    {
        List<Auction?>? auctions = _auctionsRepository.GetAuctions();
        if (auctions?.Count == 0)
            throw new CustomExceptions.NoAuctionsFoundException();
        return auctions;
    }
}