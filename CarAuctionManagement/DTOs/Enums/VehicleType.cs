namespace CarAuctionManagement.DTOs.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
public enum VehicleType
{
    Hatchback,
    Sedan,
    Suv,
    Truck
}