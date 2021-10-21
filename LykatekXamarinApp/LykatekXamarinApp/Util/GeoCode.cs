using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace LykatekXamarinApp.Util
{
    public static class GeoCode
    {
        private static string[] addressArray;

        public static async Task<string[]> getLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            Geocoder geoCoder = new Geocoder();

            if (location != null)
            {
                Position position = new Position(location.Latitude, location.Longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                string address = possibleAddresses.FirstOrDefault();
                string[] addressArray = address.Split(',');

                return addressArray;
            }
            return addressArray;
        }


    }
}
