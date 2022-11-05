using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InteractiveBorderMapApp.Entities;

namespace InteractiveBorderMapApp.Services
{
    public class CoordinateService
    {
        private const string OSM_URL = "https://api.openstreetmap.org/";
        private IHttpClientFactory _clientFactory;

        public CoordinateService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        
        public async Task<IEnumerable<OsmBuilding>> getBuildingsAsync(IEnumerable<Coordinate> area)
        {
            var list = new List<OsmBuilding>();

            var center = new Coordinate();
            var count = area.Count();
            center.Lat = area.Sum(x => x.Lat) / count;
            center.Lng = area.Sum(x => x.Lng) / count;
            var radius = 0d;
            
            foreach (var coordinate in area)
            {
                var catet1 = Math.Abs(center.Lat - coordinate.Lat);
                var catet2 = Math.Abs(center.Lng - coordinate.Lng);
                var distance = Math.Sqrt(catet1 * catet1 + catet2 * catet2);
                if (distance > radius) radius = distance;
            }

            var client = _clientFactory.CreateClient();
            var request = OSM_URL + "api/0.6/map?bbox=" + (center.Lng - radius) + 
            (center.Lat - radius) + (center.Lng + radius) + (center.Lat + radius);
            var message = await client.GetAsync(request);
            
            Console.Write(message.Content);
            //Get buildings info from message
            
            return new List<OsmBuilding>();
        }
    }
}