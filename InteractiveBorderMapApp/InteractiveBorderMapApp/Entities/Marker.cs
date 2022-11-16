using System.Text.Json.Serialization;

namespace InteractiveBorderMapApp.Entities
{
    public class Marker
    {
        [JsonPropertyName("coordinate")]
        public Coordinate Coordinate { get; set; }
        [JsonPropertyName("coordinates")]
        public Coordinate[] Coordinates { get; set; }

        [JsonPropertyName("type")]
        public string MarkerType { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        public Marker()
        {
        }

        public Marker(Coordinate coordinate, Coordinate[] coordinates, string markerType, string text)
        {
            Coordinate = coordinate;
            Coordinates = coordinates;
            MarkerType = markerType;
            Text = text;
        }
    }
}
