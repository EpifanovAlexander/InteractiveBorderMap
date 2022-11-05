using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InteractiveBorderMapApp.Entities;

namespace InteractiveBorderMapApp.Services
{
    public class CoordinateService
    {
        private const string OSM_URL = "https://api.openstreetmap.org/";
        private HashSet<string> VALID_TYPES = new HashSet<string>() {"apartments", "barracks", "bungalow", "cabin", 
            "detached", "dormitory", "farm", "ger", "hotel", "house", "houseboat", "residential", "semidetached_house",
            "static_caravan", "stilt_house", "terrace", "tree_house", "", "", ""};
        private IHttpClientFactory _clientFactory;

        public CoordinateService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        
        public async Task<IEnumerable<OsmBuilding>> getBuildingsAsync(IEnumerable<Coordinate> area)
        {
            var list = new List<OsmBuilding>();

            var maxLat = area.Max(x => x.Lat);
            var maxLng = area.Max(x => x.Lng);
            var minLat = area.Min(x => x.Lat);
            var minLng = area.Min(x => x.Lng);

            var client = _clientFactory.CreateClient();
            var request = OSM_URL + "api/0.6/map?bbox=" + minLng.ToString("G", CultureInfo.InvariantCulture) + "," +
            minLat.ToString("G", CultureInfo.InvariantCulture) + "," +
            maxLng.ToString("G", CultureInfo.InvariantCulture) + "," +
            maxLat.ToString("G", CultureInfo.InvariantCulture);
            var message = await client.GetAsync(request);
            
            
            
            return new List<OsmBuilding>();
        }
    }
}