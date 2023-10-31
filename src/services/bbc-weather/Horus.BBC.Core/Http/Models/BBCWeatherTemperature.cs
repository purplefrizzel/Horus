using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public record BBCWeatherTemperature
{
    [JsonPropertyName("C")]
    public int Celsius { get; init; }
    
    [JsonPropertyName("F")]
    public int Fahrenheit { get; init; }
}