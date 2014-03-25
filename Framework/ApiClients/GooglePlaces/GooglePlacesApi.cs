using Framework.ApiClients.GooglePlaces.Responses.DetailResponse;
using Framework.ApiClients.GooglePlaces.Responses.TextResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ApiClients.GooglePlaces
{
    public class GooglePlacesApi : BaseRestClient
    {
        public GooglePlacesApi(string key)
        {
            this.AddRequestParameter("sensor", "false");
            this.AddRequestParameter("key", key);
        }

        public async Task<GooglePlacesTextResponse> TextQueryAsync(string query)
        {
            this.baseUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";

            return await GetQueryResponse<GooglePlacesTextResponse>("query", query);
        }

        public async Task<GooglePlacesDetailResponse> DetailQueryAsync(string reference)
        {
            this.baseUrl = "https://maps.googleapis.com/maps/api/place/details/json";

            return await GetQueryResponse<GooglePlacesDetailResponse>("reference", reference);
        }

        public async Task<string> PhotoQueryAsync(string reference)
        {
            this.baseUrl = "https://maps.googleapis.com/maps/api/place/photo";

            List<RequestParameter> parameters = new List<RequestParameter>
            {
                new RequestParameter("photoreference", reference),
                new RequestParameter("maxwidth", "1600")
            };

            string url = this.GetUrlWithParameters(parameters);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowAutoRedirect = false;

            var response = await request.GetResponseAsync();

            return response.Headers["Location"];
        }

        private async Task<T> GetQueryResponse<T>(string key, string value)
        {
            string url = this.GetUrlWithParameters(key, value);

            return await JsonConvert.DeserializeObjectAsync<T>(await this.GetResponseAsync(url));
        }
    }
}
