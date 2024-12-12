using CarAuctionManagement.DTOs.Vehicles;
using CarAuctionManagement.Handlers.Vehicles.AddVehicle;
using CarAuctionManagement.Middleware;
using CarAuctionManagement.Repository.Auctions;
using CarAuctionManagement.Repository.Database;
using CarAuctionManagement.Repository.Vehicles;
using CarAuctionManagement.Services.Auctions;
using CarAuctionManagement.Services.Vehicles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use a different port
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001); // Change the port number here
});

// Add services to the container.
builder.Services.AddSingleton<IAuctionsService, AuctionsService>();
builder.Services.AddSingleton<IVehiclesService, VehiclesService>();
builder.Services.AddSingleton<IAuctionsRepository, AuctionsRepository>();
builder.Services.AddSingleton<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddSingleton<IAddVehicleHandler, AddVehicleHandler>();
builder.Services.AddSingleton<InMemoryDatabase>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Auction API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Auction API v1"));
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseHttpsRedirection();

// Define endpoints
app.MapPost("/vehicles/add", (VehicleDto vehicle, IAddVehicleHandler vehiclesHandler) =>
{
    var result = vehiclesHandler.AddVehicle(vehicle);
    return result;
});

app.MapGet("/vehicles/GetAllVehicles", (IVehiclesService vehiclesService) =>
{
    var result = vehiclesService.GetVehicles();
    return result;
});

app.MapPost("/vehicles/GetVehiclesWithFilters", (VehicleSearchDto filters, IVehiclesService vehiclesService) =>
{
    var result = vehiclesService.GetVehicleSearch(filters.Type, filters.Manufacturer, filters.Model, filters.Year);
    return result;
});


app.Run();