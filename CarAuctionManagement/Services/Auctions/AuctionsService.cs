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

    public void StartAuction(Auction auction)
    {
        auction.Validate();
        StartAuctionValidations(auction);
        _auctionsRepository.StartAuction(auction);
    }

    public List<Auction> GetAuctions()
    {
        var auctions = GetAuctionValidations();
        return auctions;
    }

    public void EndAuction(Auction auction)
    {
        EndAuctionValidations(auction);
        _auctionsRepository.EndAuction(auction);
    }

    public void PlaceBid(Bid newBid)
    {
        newBid.Validate();
        PlaceBidValidations(newBid);
        _auctionsRepository.PlaceBid(newBid);
    }

    private void PlaceBidValidations(Bid newBid)
    {
        List<Auction>? activeAuctions = GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.All(a => a.Id != newBid.AuctionId))
            throw new CustomExceptions.AuctionNotFoundException(newBid.AuctionId);
        Auction auction = activeAuctions.First(a => a.Id == newBid.AuctionId);
        if (newBid.Amount <= auction.HighestBid || newBid.Amount <= auction.Vehicle?.StartingBid)
            throw new CustomExceptions.BidAmountTooLowException(newBid.Amount, auction.HighestBid);
    }

    public List<Auction>? GetActiveAuctions()
    {
        return _auctionsRepository.GetActiveAuctions();
    }

    public List<Auction>? GetClosedAuctions()
    {
        return _auctionsRepository.GetClosedAuctions();
    }

    private void StartAuctionValidations(Auction? auction)
    {
        List<Auction> auctions = _auctionsRepository.GetAuctions();
        if (auctions.Any(a => a.Vehicle?.Id == auction?.Vehicle?.Id))
            throw new CustomExceptions.AuctionAlreadyActiveException(auction?.Vehicle?.Id);
        if (auctions.Any(a => a.Id == auction?.Id))
            throw new CustomExceptions.AuctionSameIdException(auction?.Id);
        List<Vehicle?> allVehicles = _vehiclesRepository.GetVehicles();
        if (allVehicles.All(v => v?.Id != auction?.Vehicle?.Id))
            throw new CustomExceptions.VehicleNotFoundException(auction?.Vehicle?.Id);
    }

    private void EndAuctionValidations(Auction auction)
    {
        List<Auction>? activeAuctions = GetActiveAuctions();
        if (activeAuctions == null || activeAuctions.All(a => a.Id != auction.Id))
            throw new CustomExceptions.AuctionNotFoundException(auction.Id);
    }

    private List<Auction> GetAuctionValidations()
    {
        List<Auction> auctions = _auctionsRepository.GetAuctions();
        if (auctions.Count == 0)
            throw new CustomExceptions.NoAuctionsFoundException();
        return auctions;
    }
}