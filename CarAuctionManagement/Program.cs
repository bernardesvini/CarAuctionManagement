using CarAuctionManagement.DTOs.Auctions.Requests;
using CarAuctionManagement.DTOs.Bidder.Requests;
using CarAuctionManagement.DTOs.Enums;
using CarAuctionManagement.DTOs.Vehicles.Requests;
using CarAuctionManagement.Handlers.Auctions.EndAuction;
using CarAuctionManagement.Handlers.Auctions.GetAuctions;
using CarAuctionManagement.Handlers.Auctions.PlaceBid;
using CarAuctionManagement.Handlers.Auctions.StartAuction;
using CarAuctionManagement.Handlers.Bidders.CreateBidder;
using CarAuctionManagement.Handlers.Bidders.GetBidders;
using CarAuctionManagement.Handlers.Bidders.RemoveBidders;
using CarAuctionManagement.Handlers.Vehicles.AddVehicle;
using CarAuctionManagement.Handlers.Vehicles.GetVehicles;
using CarAuctionManagement.Handlers.Vehicles.RemoveVehicle;
using CarAuctionManagement.Middleware;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Bidders;
using CarAuctionManagement.Repository.Database;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Bidders;
using CarAuctionManagement.Services.Vehicles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;


var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001); 
});

builder.Services.AddSingleton<IAuctionsService, AuctionsService>();
builder.Services.AddSingleton<IVehiclesService, VehiclesService>();
builder.Services.AddSingleton<IAuctionsRepository, AuctionsRepository>();
builder.Services.AddSingleton<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddSingleton<IBiddersService, BiddersService>();
builder.Services.AddSingleton<IBiddersRepository, BiddersRepository>();
builder.Services.AddSingleton<IAddVehicleHandler, AddVehicleHandler>();
builder.Services.AddSingleton<IGetVehiclesHandler, GetVehiclesHandler>();
builder.Services.AddSingleton<IRemoveVehicleHandler, RemoveVehicleHandler>();
builder.Services.AddSingleton<IStartAuctionHandler, StartAuctionHandler>();
builder.Services.AddSingleton<IGetAuctionHandler, GetAuctionHandler>();
builder.Services.AddSingleton<IEndAuctionHandler, EndAuctionHandler>();
builder.Services.AddSingleton<IPlaceBidHandler, PlaceBidHandler>();
builder.Services.AddSingleton<ICreateBidderHandler, CreateBidderHandler>();
builder.Services.AddSingleton<IGetBiddersHandler, GetBiddersHandler>();
builder.Services.AddSingleton<IRemoveBidderHandler, RemoveBidderHandler>();
builder.Services.AddSingleton<InMemoryDatabase>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Auction API", Version = "v1" });
    c.EnableAnnotations();
  
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Auction API v1"));
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseHttpsRedirection();

// Define endpoints
app.MapPost("/vehicles/AddVehicle", ([FromBody]VehicleRequestDto vehicle, IAddVehicleHandler vehiclesHandler) =>
{
    var result = vehiclesHandler.AddVehicle(vehicle);
    return result;
});

app.MapGet("/vehicles/GetVehicles", (int? startYear, int? endYear, Guid? id, [SwaggerParameter(Description = "Vehicle type", Required = false)] VehicleType? type, string? manufacturer, string? model, IGetVehiclesHandler vehiclesHandler) =>
{
    var result = vehiclesHandler.GetVehiclesWithFilters(startYear, endYear, id, type, manufacturer, model);
    return result;
});

app.MapPut("/vehicles/UpdateVehicle", (Guid id,[FromBody]VehicleUpdateRequestDto vehicle, IAddVehicleHandler vehiclesHandler) =>
{
    var result = vehiclesHandler.UpdateVehicle(id, vehicle);
    return result;
});

app.MapDelete("/vehicles/RemoveVehicle", ([FromBody]VehicleRemoveRequestDto vehicle, IRemoveVehicleHandler vehiclesHandler) =>
{
    vehiclesHandler.RemoveVehicle(vehicle);
});

app.MapPost("/auctions/StartAuction", ([FromBody]StartAuctionRequestDto auction, IStartAuctionHandler auctionHandler) =>
{
    var result = auctionHandler.StartAuction(auction);
    return result;
});

app.MapGet("/auctions/GetAuctions", ( IGetAuctionHandler auctionsHandler) =>
{
    var result = auctionsHandler.GetAuctions();
    return result;
});

app.MapGet("/auctions/GetActiveAuctions", (IGetAuctionHandler auctionsHandler) =>
{
    var result = auctionsHandler.GetActiveAuctions();
    return result;
});

app.MapGet("/auctions/GetClosedAuctions", (IGetAuctionHandler auctionsHandler) =>
{
    var result = auctionsHandler.GetClosedAuctions();
    return result;
});

app.MapPost("/auctions/EndAuction", ([FromBody]EndAuctionRequestDto auction, IEndAuctionHandler auctionHandler) =>
{
    auctionHandler.EndAuction(auction);
    return Results.Ok();
});

app.MapPost("/auctions/PlaceBid", ([FromBody]PlaceBidRequestDto bid, IPlaceBidHandler bidHandler) =>
{
    var result = bidHandler.PlaceBid(bid);
    return result;
});

app.MapPost("/bidders/CreateBidder", ([FromBody]CreateBidderRequestDto bidder, ICreateBidderHandler bidderHandler) =>
{
    var result = bidderHandler.CreateBidder(bidder);
    return result;
});

app.MapGet("/bidders/GetBidders", (IGetBiddersHandler bidderHandler) =>
{
    var result = bidderHandler.GetBidders();
    return result;
});

app.MapGet("/bidders/GetBidderById", (Guid bidderId, IGetBiddersHandler bidderHandler) =>
{
    var result = bidderHandler.GetBidderById(bidderId);
    return result;
});

app.MapPut("/bidders/UpdateBid", ([FromBody]UpdateBidderRequestDto bidder, ICreateBidderHandler bidderHandler) =>
{
    var result = bidderHandler.UpdateBidder(bidder);
    return result;
});

app.MapDelete("/bidders/RemoveBidder", ([FromBody]RemoveBidderRequestDto bidder, IRemoveBidderHandler bidderHandler) =>
{
    bidderHandler.RemoveBidder(bidder.Id);
});

app.Run();