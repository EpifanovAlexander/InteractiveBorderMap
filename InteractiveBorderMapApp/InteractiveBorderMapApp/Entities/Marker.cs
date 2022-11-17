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
        
        [JsonPropertyName("showMarker")]
        public bool isShowMarker { get; set; }

        public Marker(bool isShowMarker)
        {
            this.isShowMarker = isShowMarker;
        }

        public Marker(Coordinate coordinate, Coordinate[] coordinates, string markerType, string text, bool isShowMarker)
        {
            Coordinate = coordinate;
            Coordinates = coordinates;
            MarkerType = markerType;
            Text = text;
            this.isShowMarker = isShowMarker;
        }
    }
}
