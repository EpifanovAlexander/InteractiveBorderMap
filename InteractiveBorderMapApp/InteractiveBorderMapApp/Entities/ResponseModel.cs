using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InteractiveBorderMapApp.Entities
{
    public class ResponseModel
    {
        [JsonPropertyName("markers")]
        public List<Marker> Markers { get; set; }
        [JsonPropertyName("reportId")]
        public string ReportId { get; set; }
        public ResponseModel(List<Marker> markers, string reportId)
        {
            Markers = markers;
            ReportId = reportId;
        }
    }
}
