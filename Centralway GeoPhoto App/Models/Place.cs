﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.ApiClients.GooglePlaces.Responses;
using Framework.ApiClients.GooglePlaces.Responses.TextResponse;
using Framework.ApiClients.GooglePlaces.Responses.DetailResponse;

namespace Centralway_GeoPhoto_App.Models
{
    public class Place
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Reference { get; set; }

        public PlaceLocation Location { get; set; }

        public List<string> Types { get; set; }

        public bool HasPhotos { get; set; }

        public static Place FromTextResponse(Framework.ApiClients.GooglePlaces.Responses.TextResponse.Result result)
        {
            var place = new Place
            {
                Name = result.name,
                Address = result.formatted_address,
                Reference = result.reference,
                Location = PlaceLocation.FromApiGeometry(result.geometry),
                Types = result.types,
                HasPhotos = (result.photos != null) ? true : false
            };
            return place;
        }

        public static Place FromDetailResponse(GooglePlacesDetailResponse response)
        {
            var result = response.result;

            var place = new Place
            {
                Name = result.name,
                Address = result.formatted_address,
                Reference = result.reference,
                Location = PlaceLocation.FromApiGeometry(result.geometry),
                Types = result.types,
                HasPhotos = (result.photos != null) ? true : false
            };

            return place;
        }
    }

    public class PlaceLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static PlaceLocation FromApiGeometry(Geometry geometry)
        {
            return new PlaceLocation
            {
                Latitude = geometry.location.lat,
                Longitude = geometry.location.lng
            };
        }
    }
}