using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    internal static class HTTPHelper
    {
        static string baseURL = "http://103.228.144.162:4533/rest/";
        static RestClient restClient;

        static HTTPHelper()
        {
            restClient= new RestClient();
        }

        internal static string BuildURL(string endpoint, string appendQueryStringURL)
        {
            return baseURL + endpoint + "?u=admin&p=admin&v=1.12.0&c=myapp2&" + appendQueryStringURL;
        }

        internal static async Task<string> GetXML(string endpoint,string appendQueryStringURL="")
        {
            string url = BuildURL(endpoint,appendQueryStringURL);
            var request = new RestRequest(url);
            var response = await restClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            return string.Empty;
        }

    }
}
