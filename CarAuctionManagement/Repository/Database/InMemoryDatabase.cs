using CarAuctionManagement.Models.Auctions;
using CarAuctionManagement.Models.Vehicles;

namespace CarAuctionManagement.Repository.Database;

public class InMemoryDatabase
{
    public List<Auction?>? Auctions { get; set; } = new List<Auction?>();
    public List<Vehicle?> Vehicles { get; set; } = new List<Vehicle?>();
}