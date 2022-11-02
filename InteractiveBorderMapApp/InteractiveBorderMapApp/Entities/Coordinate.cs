using System.Text.Json.Serialization;

namespace InteractiveBorderMapApp.Entities
{
    public class Coordinate
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        public Coordinate()
        {
        }
    }
}
