using FlickrNet;
using Framework.ApiClients.GooglePlaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Centralway_GeoPhoto_App.Controllers
{
    public class PhotosController : ApiController
    {
        public async Task<IEnumerable<Models.Photo>> GetPlacePhotos(string reference)
        {
            GooglePlacesApi placeClient = new GooglePlacesApi(ConfigurationManager.AppSettings["GoogleAPIKey"]);

            var photoReferences = (await placeClient.DetailQueryAsync(reference)).result.photos.Select(p => p.photo_reference);

            var photos = photoReferences.AsParallel().Select(async pr => new Models.Photo(await placeClient.PhotoQueryAsync(pr)));

            return Task.WhenAll(photos).Result;
        }

        public IEnumerable<Models.Photo> GetNearbyPhotos(double latitude, double longitude)
        {
            Flickr flickrClient = new Flickr(ConfigurationManager.AppSettings["FlickrAPIKey"], ConfigurationManager.AppSettings["FlickrSecret"]);

            var searchOptions = new PhotoSearchOptions();

            searchOptions.Latitude = latitude;
            searchOptions.Longitude = longitude;
            searchOptions.Radius = 1.0f;
            searchOptions.RadiusUnits = RadiusUnit.Kilometers;

            var photos = flickrClient.PhotosSearch(searchOptions).Take(10);

            return photos.Select(p => Models.Photo.FromFlickrPhoto(p));
        }
    }
}
