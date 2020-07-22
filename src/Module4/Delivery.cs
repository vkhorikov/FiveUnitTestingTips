namespace Module4
{
    // Domain entity
    public class Delivery
    {
        public long Id { get; private set; } // Set up by the ORM
        public string Address { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public Delivery(string address, double latitude, double longitude)
        {
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
