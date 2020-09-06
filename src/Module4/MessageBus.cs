namespace Module4
{
    public interface IMessageBusGateway
    {
        void SendMessage(string message);
    }

    public class MessageBusGateway : IMessageBusGateway
    {
        private readonly BusClient _busClient;

        public MessageBusGateway(string connectionString)
        {
            _busClient = new BusClient(connectionString);
        }

        public void SendMessage(string message)
        {
            _busClient.SendMessage(message);
        }
    }

    public class MessageBus
    {
        private readonly IMessageBusGateway _gateway;

        public MessageBus(IMessageBusGateway gateway)
        {
            _gateway = gateway;
        }

        public void SendNewShippingMessage(long deliveryId, Coordinates coordinates)
        {
            _gateway.SendMessage(
                "Type: NewShipping;" +
                $"Id: {deliveryId};" +
                $"Latitude: {coordinates.Latitude};" +
                $"Longitude: {coordinates.Longitude}");
        }

        public void SendShippingIsCancelledMessage(long deliveryId, Coordinates coordinates)
        {
            _gateway.SendMessage(
                "Type: ShippingIsCancelled;" +
                $"Id: {deliveryId};" +
                $"Latitude: {coordinates.Latitude};" +
                $"Longitude: {coordinates.Longitude}");
        }
    }
}
