﻿using FluentValidation;

namespace CarAuctionManagement.DTOs.Auctions.Requests;

public class EndAuctionRequestDto
{
    public Guid AuctionId { get; set; }
    
    public class EndAuctionRequestDtoValidator : AbstractValidator<EndAuctionRequestDto>
    {
        public EndAuctionRequestDtoValidator()
        {
            RuleFor(x => x.AuctionId).NotNull().NotEmpty().WithMessage("Auction ID must be provided to end a auction.");
        }
    }
}