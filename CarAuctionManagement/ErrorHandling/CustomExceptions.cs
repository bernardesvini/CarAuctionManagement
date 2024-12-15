namespace CarAuctionManagement.ErrorHandling;

public abstract class CustomExceptions
{
    public class VehicleNotFoundException(Guid? vehicleId) : Exception($"Vehicle with ID {vehicleId} not found.");
    public class VehicleAlreadyExistsException(Guid? vehicleId) : Exception($"Vehicle with ID {vehicleId} already exists on the inventory.");
    public class VehicleDataNullException() : Exception("Vehicle need to have data.");
    public class NoVehiclesFoundException() : Exception("No vehicles found in the inventory.");
    public class NoVehiclesFoundWithFiltersException() : Exception("No vehicles with the specified filters found.");
    public class AuctionAlreadyActiveException(Guid? vehicleId) : Exception($"An auction is already active for vehicle ID {vehicleId}.");
    public class NoAuctionsFoundException() : Exception("No auctions found.");
    public class NoClosedAuctionsFoundException() : Exception("No closed auctions found.");
    public class NoActiveAuctionsFoundException() : Exception("No active auctions found.");
    public class AuctionNotFoundException(Guid? auctionId) : Exception($"Auction with ID {auctionId} not found or isn't active.");
    public class BidAmountTooLowException(decimal? bidAmount, decimal? highestBid) : Exception($"The bid amount {bidAmount} is lower or equal to the current highest bid {highestBid}.");
    public class ValidationException(string? message) : Exception(message);
    public class AuctionSameIdException(Guid? auctionId) : Exception($"An auction with the ID {auctionId} already exists.");
    public class InvalidVehicleTypeException() : Exception("This type of vehicle is unknown.");
    public class BidderAlreadyExistsException(string? getEmail, string? getName) : Exception($"A bidder with the email {getEmail} and name {getName} already exists.");
    public class BidderNotFoundByIdException(Guid? id) : Exception($"A bidder with the ID {id} was not found.");
    public class BidderNotFoundException() : Exception("No bidders found.");
    public class NoHighestBidderException(Guid? auctionId) : Exception($"No bidder found for auction with ID {auctionId}.");
    public class VehicleHaveActiveAuctionException(Guid? vehicleId, Guid? auctionId) : Exception ($"Vehicle with ID {vehicleId} have an active auction with ID {auctionId}, so cannot be updated.");
    
}