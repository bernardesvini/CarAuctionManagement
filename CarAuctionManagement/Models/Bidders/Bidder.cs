using CarAuctionManagement.DTOs.Bidder.Responses;
using CarAuctionManagement.ErrorHandling;

namespace CarAuctionManagement.Models.Bidders;

public class Bidder
{
    private Guid? Id { get; set; }
    private string? Name { get; set; }
    private string? Email { get; set; }
    private bool IsDeleted { get; set; }
    
    
    public Bidder(Guid? id, string? name, string? email)
    {
        Id = id;
        Name = name;
        Email = email;
        IsDeleted = false;
        Validate();
    }
    
    public Guid? GetId() => Id;
    public string? GetName() => Name;
    public string? GetEmail() => Email;
    public void SetIsDeleted(bool isDeleted) => IsDeleted = isDeleted;
    public bool GetIsDeleted() => IsDeleted;
    
    public BidderResponseDto? ToResponseDto()
    {
        return new BidderResponseDto
        {
            Id = GetId(),
            Name = GetName(),
            Email = GetEmail()
        };
    }
    
    public void Validate()
    {
        if (Guid.Empty.Equals(Id))
        {
            throw new CustomExceptions.ValidationException("Id must be provided.");
        }

        if (string.IsNullOrEmpty(Name))
        {
            throw new CustomExceptions.ValidationException("Name must be provided.");
        }

        if (string.IsNullOrEmpty(Email))
        {
            throw new CustomExceptions.ValidationException("Email must be provided.");
        }
    }
}