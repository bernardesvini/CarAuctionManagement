using CarAuctionManagement.ErrorHandling;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace CarAuctionManagement.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Default values
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "An unexpected error occurred.";
        object? response = null;

        // Handle CustomExceptions
        switch (exception)
        {
            case FluentValidation.ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                var errors = validationException.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                });
                response = new
                {
                    StatusCode = statusCode,
                    Errors = errors
                };
                break;

            case CustomExceptions.VehicleNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case CustomExceptions.VehicleAlreadyExistsException:
                statusCode = StatusCodes.Status409Conflict;
                message = exception.Message;
                break;

            case CustomExceptions.VehicleDataNullException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;

            case CustomExceptions.NoVehiclesFoundException:
            case CustomExceptions.NoVehiclesFoundWithFiltersException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case CustomExceptions.AuctionAlreadyActiveException:
                statusCode = StatusCodes.Status409Conflict;
                message = exception.Message;
                break;

            case CustomExceptions.NoAuctionsFoundException:
            case CustomExceptions.AuctionNotFoundException:
            case CustomExceptions.NoClosedAuctionsFoundException:
            case CustomExceptions.NoActiveAuctionsFoundException:
            case CustomExceptions.BidderNotFoundException:
            case CustomExceptions.BidderNotFoundByIdException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case CustomExceptions.BidAmountTooLowException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;

            case CustomExceptions.ValidationException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;

            case CustomExceptions.AuctionSameIdException:
            case CustomExceptions.BidderAlreadyExistsException:
                statusCode = StatusCodes.Status409Conflict;
                message = exception.Message;
                break;

            case CustomExceptions.InvalidVehicleTypeException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        if (response == null)
        {
            response = new
            {
                StatusCode = statusCode,
                Message = message
            };
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}