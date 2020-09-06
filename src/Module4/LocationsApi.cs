using CSharpFunctionalExtensions;

namespace Module4
{
    public class LocationsApi
    {
        private readonly ILocationsApiGateway _gateway;

        public LocationsApi(ILocationsApiGateway gateway)
        {
            _gateway = gateway;
        }

        public Result<Coordinates> GetCoordinatesByAddress(string address)
        {
            LocationsClientResponse response = _gateway.GetCoordinatesByAddress(address);
            
            if (response == null)
                return Result.Failure<Coordinates>($"No latitude / longitude available for the address '{address}'");

            Coordinates coordinates = Coordinates.Create(response.Latitude, response.Longitude).Value;
            return Result.Ok(coordinates);
        }
    }

    public class LocationsApiGateway : ILocationsApiGateway
    {
        private readonly LocationsClient _locationsClient;

        public LocationsApiGateway(string endpointUrl)
        {
            _locationsClient = new LocationsClient(endpointUrl);
            // Any other initialization logic goes here
        }

        public LocationsClientResponse GetCoordinatesByAddress(string address)
        {
            return _locationsClient.GetCoordinatesByAddress(address);
        }
    }

    public interface ILocationsApiGateway
    {
        LocationsClientResponse GetCoordinatesByAddress(string address);
    }
}
