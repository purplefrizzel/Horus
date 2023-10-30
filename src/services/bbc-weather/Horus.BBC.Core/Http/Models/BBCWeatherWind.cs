using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public record BBCWeatherWind
{
    [JsonPropertyName("windSpeedMph")]
    public int SpeedMph { get; init; }
    
    [JsonPropertyName("windSpeedKmh")]
    public int SpeedKmh { get; init; }
    
    [JsonPropertyName("windDirection")]
    public string? Direction { get; init; }
    
    [JsonPropertyName("windDirectionAbbreviation")]
    public string? DirectionAbbreviation { get; init; }
    
    [JsonPropertyName("windDirectionFull")]
    public string? DirectionFull { get; init; }
}