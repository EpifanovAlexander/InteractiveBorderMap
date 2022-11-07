﻿using System.Text.Json.Serialization;

namespace InteractiveBorderMapApp.Entities
{
    public class Marker
    {
        [JsonPropertyName("coordinate")]
        public Coordinate Coordinate { get; set; }

        [JsonPropertyName("type")]
        public string MarkerType { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        public Marker()
        {
        }

        public Marker(Coordinate coordinate, string markerType, string text)
        {
            Coordinate = coordinate;
            MarkerType = markerType;
            Text = text;
        }
    }
}