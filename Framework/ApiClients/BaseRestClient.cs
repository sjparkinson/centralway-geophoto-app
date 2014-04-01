using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace Framework.ApiClients
{
    public struct RequestParameter
    {
        public readonly string Key;

        public readonly string Value;

        public RequestParameter(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public abstract class BaseRestClient
    {
        private readonly List<RequestParameter> requestParameters = new List<RequestParameter>();

        protected string baseUrl;

        protected List<RequestParameter> GetRequestParameters()
        {
            return this.requestParameters;
        }

        protected BaseRestClient AddRequestParameter(string key, string value)
        {
            this.requestParameters.Add(new RequestParameter(key, value));

            return this;
        }

        protected BaseRestClient AddRequestParameter(RequestParameter parameter)
        {
            this.requestParameters.Add(parameter);

            return this;
        }

        protected async Task<string> GetResponseAsync(string url)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        protected string GetUrlWithParameters()
        {
            var safeParameters = this.GetRequestParameters().Select(p => GetSafeParameterPair(p.Key, p.Value));

            return this.baseUrl + "?" + string.Join("&", safeParameters);
        }

        protected string GetUrlWithParameters(string key, string value)
        {
            return string.Format("{0}&{1}", this.GetUrlWithParameters(), GetSafeParameterPair(key, value));
        }

        protected string GetUrlWithParameters(IEnumerable<RequestParameter> parameters)
        {
            var safeParameters = parameters.Select(p => GetSafeParameterPair(p.Key, p.Value));

            return this.GetUrlWithParameters() + "&" + string.Join("&", safeParameters);
        }

        private static string GetSafeParameterPair(string key, string value)
        {
            return string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
        }
    }
}
