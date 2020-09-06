using CSharpFunctionalExtensions;

namespace Module4
{
    public class DeliveryController
    {
        private readonly LocationsApi _locationsApi;
        private readonly MessageBus _messageBus;

        public DeliveryController(
            ILocationsApiGateway locationsApiGateway, IMessageBusGateway messageBusGateway)
        {
            _locationsApi = new LocationsApi(locationsApiGateway);
            _messageBus = new MessageBus(messageBusGateway);
        }

        public Envelope NewShipping(string address)
        {
            Result<Coordinates> result = _locationsApi.GetCoordinatesByAddress(address);
            if (result.IsFailure)
                return Envelope.Error(result.Error);

            Coordinates coordinates = result.Value;

            var delivery = new Delivery(address, coordinates);
            SaveDelivery(delivery);

            _messageBus.SendNewShippingMessage(delivery.Id, coordinates);

            return Envelope.Ok(delivery.Id);
        }

        public Envelope CancelShipping(long deliveryId)
        {
            Delivery delivery = GetDelivery(deliveryId);

            // mark the delivery as cancelled

            _messageBus.SendShippingIsCancelledMessage(delivery.Id, delivery.Coordinates);

            return Envelope.Ok();
        }

        private void SaveDelivery(Delivery delivery)
        {
            // Use a repository/ORM
        }

        private Delivery GetDelivery(long id)
        {
            // Use a repository/ORM
            return new Delivery("Address", Coordinates.Create(38.889248, -77.050636).Value)
            {
                Id = 1
            };
        }
    }
}
