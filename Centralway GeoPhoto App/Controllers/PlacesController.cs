using Centralway_GeoPhoto_App.Models;
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
    public class PlacesController : ApiController
    {
        private readonly GooglePlacesApi placeClient = new GooglePlacesApi(ConfigurationManager.AppSettings["GoogleAPIKey"]);

        public async Task<IEnumerable<Place>> GetPlacesByText(string query)
        {
            var places = await placeClient.TextQueryAsync(query);

            return places.results.Select(r => Place.FromTextResponse(r));
        }

        public async Task<Place> GetPlaceByReference(string reference)
        {
            var place = await placeClient.DetailQueryAsync(reference);

            return Place.FromDetailResponse(place);
        }
    }
}
