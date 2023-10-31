using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public class BBCWeatherStation
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
    
    [JsonPropertyName("distance")]
    public BBCWeatherStationDistance? Distance { get; init; }
}