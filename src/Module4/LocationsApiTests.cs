using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Module4
{
    public class LocationsApiTests
    {
        [Fact]
        public void Null_response_means_address_is_invalid()
        {
            var stub = new Mock<ILocationsApiGateway>();
            stub.Setup(x => x.GetCoordinatesByAddress(It.IsAny<string>()))
                .Returns((LocationsClientResponse)null);
            var api = new LocationsApi(stub.Object);

            Result<Coordinates> result = api.GetCoordinatesByAddress("Address");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("No latitude / longitude available for the address 'Address'");
        }

        [Fact]
        public void Invalid_coordinates_lead_to_an_unhandled_exception()
        {
        }

        [Fact]
        public void Error_response_leads_to_an_unhandled_exception()
        {
        }
    }
}
