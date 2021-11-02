using Microsoft.Extensions.Configuration;
using Model_Accesse.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL
{
    public class MapQuest
    {
        private string apikey;
        public MapQuest(IConfiguration configuration)
        {
            apikey = configuration["ApiKey:Mapquest"];
        }

        public async Task<JObject> GetRouteData(Tour tour)
        {
            HttpClient httpClient = new HttpClient();
            Uri uri = new Uri($"http://www.mapquestapi.com/directions/v2/route" +
                $"?key={apikey}&unit=k&to={tour.destination}&from={tour.location}");
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var route = JObject.Parse(body);
                int errorCode = route.SelectToken("route.routeError").Value<int>("errorCode");
                if (errorCode < 0)
                {
                    return route;
                }
            }
            return null;
        }
        public int GetDistance(JObject route)
        {
            return route.SelectToken("route.distance").Value<Int32>();
        }

        public string GetImage(Tour tour)
        {
            return $"https://www.mapquestapi.com/staticmap/v5/map" +
                $"?key={apikey}&unit=k&start={tour.location}&end={tour.destination}";
        }
        // Get Distance
        // Get Image
    }
}
