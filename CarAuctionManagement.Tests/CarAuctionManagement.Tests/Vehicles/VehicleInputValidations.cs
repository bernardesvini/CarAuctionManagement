using CarAuctionManagement.DTOs.Vehicles.Requests;
using FluentValidation.TestHelper;

namespace CarAuctionManagement.Tests.Vehicles.Validations
{
    public class VehicleSearchRequestDtoValidatorTests
    {
        private readonly VehicleSearchRequestDto.VehicleSearchRequestDtoValidator _validator;

        public VehicleSearchRequestDtoValidatorTests()
        {
            _validator = new VehicleSearchRequestDto.VehicleSearchRequestDtoValidator();
        }

        [Theory]
        [InlineData(1800, "Start year must be valid.")]
        [InlineData(3000, "Start year must be valid.")]
        public void Should_Have_Error_When_StartYear_Is_Invalid(int startYear, string expectedErrorMessage)
        {
            var dto = new VehicleSearchRequestDto { StartYear = startYear };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.StartYear).WithErrorMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData(1800, "End year must be valid.")]
        [InlineData(3000, "End year must be valid.")]
        public void Should_Have_Error_When_EndYear_Is_Invalid(int endYear, string expectedErrorMessage)
        {
            var dto = new VehicleSearchRequestDto { EndYear = endYear };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.EndYear).WithErrorMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData("InvalidType", "Vehicle type must be valid.")]
        public void Should_Have_Error_When_Type_Is_Invalid(string type, string expectedErrorMessage)
        {
            var dto = new VehicleSearchRequestDto { Type = type };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Type).WithErrorMessage(expectedErrorMessage);
        }
    }
}