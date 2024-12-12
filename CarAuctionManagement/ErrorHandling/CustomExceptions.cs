namespace CarAuctionManagement.ErrorHandling;

public abstract class CustomExceptions
{
    public class VehicleNotFoundException(string? vehicleId) : Exception($"Vehicle with ID {vehicleId} not found.");
    public class VehicleAlreadyExistsException(string? vehicleId) : Exception($"Vehicle with ID {vehicleId} already exists on the inventory.");
    public class VehicleDataNullException() : Exception("Vehicle need to have data.");
    public class NoVehiclesFoundException() : Exception("No vehicles found on the inventory.");
    public class NoVehiclesFoundWithFiltersException() : Exception("No vehicles with the specified filters found.");
    public class AuctionAlreadyActiveException(string? vehicleId) : Exception($"An auction is already active for vehicle ID {vehicleId}.");
    public class NoAuctionsFoundException() : Exception("No auctions found.");
    public class AuctionNotFoundException(string? auctionId) : Exception($"Auction with ID {auctionId} not found or isn't active.");
    public class BidAmountTooLowException(decimal? bidAmount, decimal? highestBid) : Exception($"The bid amount {bidAmount} is lower or equal to the current highest bid {highestBid}.");
    public class ValidationException(string? message) : Exception(message);
    public class AuctionSameIdException(string? auctionId) : Exception($"An auction with the ID {auctionId} already exists.");
    public class InvalidVehicleTypeException() : Exception("This type of vehicle is unknown.");

}