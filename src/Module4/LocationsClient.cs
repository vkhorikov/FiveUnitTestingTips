namespace Module4
{
    // Class from a NuGet package that reaches out to an external API
    public class LocationsClient
    {
        public LocationsClient(string endpointUrl)
        {
        }

        public LocationsClientResponse GetCoordinatesByAddress(string address)
        {
            return new LocationsClientResponse
            {
                Latitude = 38.889248,
                Longitude = -77.050636,
                UtmEasting = 322147.22,
                UtmNorthing = 4306485.06
            };
        }
    }

    public class LocationsClientResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double UtmEasting { get; set; }
        public double UtmNorthing { get; set; }
    }
}
