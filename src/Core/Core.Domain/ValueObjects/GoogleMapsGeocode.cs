using Core.Domain.Models;

namespace Core.Domain.ValueObjects
{
    public class GoogleMapsGeocode : ValueObject<GoogleMapsGeocode>
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        protected GoogleMapsGeocode() { }

        public GoogleMapsGeocode(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude};{Longitude}";
        }
    }
}