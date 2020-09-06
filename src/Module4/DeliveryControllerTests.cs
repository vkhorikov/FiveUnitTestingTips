using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using Xunit;

namespace Module4
{
    public class DeliveryControllerTests
    {
        [Fact]
        public void Canceling_a_shipping()
        {
            Delivery delivery = CreateDelivery();
            var messageBusMock = new Mock<IMessageBusGateway>();
            var controller = new DeliveryController(null, messageBusMock.Object);

            Envelope envelope = controller.CancelShipping(delivery.Id);

            envelope.IsError.Should().BeFalse();
            // Check the delivery state in the db
            messageBusMock.Verify(x => x.SendMessage("Type: ShippingIsCancelled;" +
                $"Id: {delivery.Id};" +
                $"Latitude: {delivery.Coordinates.Latitude};" +
                $"Longitude: {delivery.Coordinates.Longitude}"));
        }

        [Fact]
        public void Canceling_a_shipping_with_spy()
        {
            Delivery delivery = CreateDelivery();
            var messageBusSpy = new MessageBusGatewaySpy();
            var controller = new DeliveryController(null, messageBusSpy);

            Envelope envelope = controller.CancelShipping(delivery.Id);

            envelope.IsError.Should().BeFalse();
            // Check the delivery state in the db
            messageBusSpy.ShouldSendNumberOfMessages(1)
                .WithShippingIsCancelledMessage(
                    delivery.Id,
                    delivery.Coordinates.Latitude,
                    delivery.Coordinates.Longitude);
        }

        [Fact]
        public void Creating_a_new_shipping()
        {
            var messageBusSpy = new MessageBusGatewaySpy();
            var stub = new Mock<ILocationsApiGateway>();
            stub.Setup(x => x.GetCoordinatesByAddress(It.IsAny<string>()))
                .Returns(new LocationsClientResponse
                {
                    Latitude = 38.889248,
                    Longitude = -77.050636
                });
            var controller = new DeliveryController(stub.Object, messageBusSpy);

            Envelope envelope = controller.NewShipping("Some address");

            envelope.IsError.Should().BeFalse();
            long deliveryId = (long)envelope.Result;
            // Check the delivery state in the db using deliveryId
            messageBusSpy.ShouldSendNumberOfMessages(1)
                .WithNewShippingMessage(deliveryId, 38.889248, -77.050636);
        }

        private Delivery CreateDelivery()
        {
            // Save in the database
            return new Delivery("Address", Coordinates.Create(38.889248, -77.050636).Value)
            {
                Id = 1
            };
        }
    }

    public class MessageBusGatewaySpy : IMessageBusGateway
    {
        private readonly List<string> _sentMessages = new List<string>();

        public void SendMessage(string message)
        {
            _sentMessages.Add(message);
        }

        public MessageBusGatewaySpy ShouldSendNumberOfMessages(int number)
        {
            _sentMessages.Count.Should().Be(number);
            return this;
        }

        public MessageBusGatewaySpy WithShippingIsCancelledMessage(
            long deliveryId, double latitude, double longitude)
        {
            string message = "Type: ShippingIsCancelled;" +
                $"Id: {deliveryId};" +
                $"Latitude: {latitude};" +
                $"Longitude: {longitude}";
            _sentMessages.Should().Contain(message);

            return this;
        }

        public MessageBusGatewaySpy WithNewShippingMessage(
            long deliveryId, double latitude, double longitude)
        {
            string message = "Type: NewShipping;" +
                $"Id: {deliveryId};" +
                $"Latitude: {latitude};" +
                $"Longitude: {longitude}";
            _sentMessages.Should().Contain(message);

            return this;
        }
    }
}
