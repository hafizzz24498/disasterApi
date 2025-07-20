using System.Text.Json.Serialization;

namespace disasterApi.Core.Dtos
{
    public class EarthQuakeResponseDto
    {
        [JsonPropertyName("features")]
        public List<Feature>? Features { get; set; }
    }

    public class Feature
    {
        [JsonPropertyName("properties")]
        public EarthquakeProperties? Properties { get; set; }
    }

    public class EarthquakeProperties
    {
        [JsonPropertyName("mag")]
        public double? Mag { get; set; }

        [JsonPropertyName("place")]
        public string? Place { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}
