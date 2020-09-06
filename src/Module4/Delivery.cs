using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Module4
{
    // Domain entity
    public class Delivery
    {
        public long Id { get; set; } // Set up by the ORM
        public string Address { get; }
        public Coordinates Coordinates { get; }

        public Delivery(string address, Coordinates coordinates)
        {
            Address = address;
            Coordinates = coordinates;
        }
    }

    public class Coordinates : ValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        private Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Result<Coordinates> Create(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null)
                return Result.Failure<Coordinates>("Latitude and Longitude must be specified");

            if (latitude.Value < -90 || latitude.Value > 90)
                return Result.Failure<Coordinates>("Latitude must be between -90 and 90");

            if (longitude.Value < -180 || longitude.Value > 180)
                return Result.Failure<Coordinates>("Longitude must be between -180 and 180");

            return Result.Ok(new Coordinates(latitude.Value, longitude.Value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
