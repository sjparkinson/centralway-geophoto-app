using Centralway_GeoPhoto_App.Models;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Places.Request;
using GoogleMapsApi.Entities.PlacesDetails.Request;
using GoogleMapsApi.Entities.PlacesDetails.Response;
using GoogleMapsApi.Entities.PlacesText.Request;
using GoogleMapsApi.Entities.PlacesText.Response;
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
    public class PlacesController : ApiController
    {
        private readonly string googleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        // GET api/places?query=<query>
        public IEnumerable<Place> Get(string query)
        {
            PlacesTextRequest placesRequest = new PlacesTextRequest()
            {
                ApiKey = googleAPIKey,
                Query = query
            };

            var response = GoogleMaps.PlacesText.Query(placesRequest);

            return response.Results.Select(r => Place.FromSearchResult(r));
        }
    }
}
