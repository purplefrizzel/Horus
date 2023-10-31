using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public record BBCWeatherStationDistance
{
    [JsonPropertyName("km")]
    public double Kilometers { get; init; }
    
    [JsonPropertyName("miles")]
    public double Miles { get; init; }
}