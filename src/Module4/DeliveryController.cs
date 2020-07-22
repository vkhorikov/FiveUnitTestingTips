namespace Module4
{
    public class DeliveryController
    {
        private readonly LocationsClient _locationsClient;
        private readonly BusClient _busClient;

        public DeliveryController(LocationsClient locationsClient, BusClient busClient)
        {
            _locationsClient = locationsClient;
            _busClient = busClient;
        }

        public Envelope NewShipping(string address)
        {
            LocationsClientResponse response = _locationsClient.GetCoordinatesByAddress(address);

            var delivery = new Delivery(address, response.Latitude, response.Longitude);
            SaveDelivery(delivery);

            _busClient.SendMessage(
                "Type: NewShipping;" +
                $"Latitude: {response.Latitude};" +
                $"Longitude: {response.Longitude}");

            return Envelope.Ok(delivery.Id);
        }

        private void SaveDelivery(Delivery delivery)
        {
            // Use a repository/ORM
        }
    }
}
