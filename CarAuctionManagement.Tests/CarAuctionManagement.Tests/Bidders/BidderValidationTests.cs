using CarAuctionManagement.DTOs.Bidder.Requests;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CarAuctionManagement.Tests.Bidders
{
    public class BidderValidationTests
    {
        private readonly CreateBidderRequestDto.CreateBidderRequestDtoValidator _createValidator;
        private readonly UpdateBidderRequestDto.UpdateBidderRequestDtoValidator _updateValidator;
        private readonly IValidator<RemoveBidderRequestDto> _removeValidator;

        public BidderValidationTests()
        {
            _createValidator = new CreateBidderRequestDto.CreateBidderRequestDtoValidator();
            _updateValidator = new UpdateBidderRequestDto.UpdateBidderRequestDtoValidator();
            _removeValidator = new InlineValidator<RemoveBidderRequestDto>
            {
                v => v.RuleFor(x => x.Id).NotNull().WithMessage("Bidder ID must be provided.")
            };
        }

        [Theory]
        [InlineData(null, "Bidder name must be provided.")]
        [InlineData("", "Bidder name must be provided.")]
        public void CreateBidder_ShouldHaveError_WhenNameIsInvalid(string name, string expectedErrorMessage)
        {
            var model = new CreateBidderRequestDto { Name = name };
            var result = _createValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData("", "A valid email must be provided.")]
        [InlineData("invalid-email", "A valid email must be provided.")]
        public void CreateBidder_ShouldHaveError_WhenEmailIsInvalid(string email, string expectedErrorMessage)
        {
            var model = new CreateBidderRequestDto { Email = email };
            var result = _createValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData("invalid-email", "A valid email must be provided.")]
        public void UpdateBidder_ShouldHaveError_WhenEmailIsInvalid(string email, string expectedErrorMessage)
        {
            var model = new UpdateBidderRequestDto { Email = email };
            var result = _updateValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedErrorMessage);
        }

        [Fact]
        public void RemoveBidder_ShouldHaveError_WhenIdIsNull()
        {
            var model = new RemoveBidderRequestDto { Id = null };
            var result = _removeValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Bidder ID must be provided.");
        }

        [Fact]
        public void RemoveBidder_ShouldNotHaveError_WhenIdIsValid()
        {
            var model = new RemoveBidderRequestDto { Id = Guid.NewGuid() };
            var result = _removeValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}