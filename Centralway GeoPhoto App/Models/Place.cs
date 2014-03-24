using GoogleMapsApi.Entities.PlacesText.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchResult = GoogleMapsApi.Entities.PlacesText.Response.Result;

namespace Centralway_GeoPhoto_App.Models
{
    public class Place
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public Location Location { get; set; }

        public static Place FromSearchResult(SearchResult result)
        {
            return new Place
            {
                Name = result.Name,
                Address = result.FormattedAddress,
                Location = Location.FromGeometry(result.Geometry)
            };
        }
    }

    public class Location
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static Location FromGeometry(Geometry geometry)
        {
            return new Location
            {
                Latitude = geometry.Location.Latitude,
                Longitude = geometry.Location.Longitude
            };
        }
    }
}