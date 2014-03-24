using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SearchResult = GoogleMapsApi.Entities.PlacesText.Response.Result;

namespace Centralway_GeoPhoto_App.Controllers
{
    public class PhotosController : ApiController
    {
        private readonly Flickr flickr = new Flickr(ConfigurationManager.AppSettings["FlickrAPIKey"], ConfigurationManager.AppSettings["FlickrSecret"]);

        public IEnumerable<Models.Photo> Get(double latitude, double longitude)
        {
            var searchOptions = new PhotoSearchOptions();

            searchOptions.Latitude = latitude;
            searchOptions.Longitude = longitude;
            searchOptions.Radius = 1.0f;
            searchOptions.RadiusUnits = RadiusUnit.Kilometers;

            var photos = flickr.PhotosSearch(searchOptions).Take(10);

            return photos.Select(p => Models.Photo.FromFlickrPhoto(p));
        }
    }
}
