using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public record BBCWeatherObservation
{
    [JsonPropertyName("localTime")]
    public string? LocalTime { get; init; }
    
    [JsonPropertyName("localDate")]
    public DateOnly LocalDate { get; init; }
    
    [JsonPropertyName("humidityPercent")]
    public int HumidityPercent { get; init; }
    
    [JsonPropertyName("pressureMb")]
    public int PressureMb { get; init; }
    
    [JsonPropertyName("pressureDirection")]
    public string? PressureDirection { get; init; }
    
    [JsonPropertyName("visibility")]
    public int? Visibility { get; init; }
    
    [JsonPropertyName("temperature")]
    public BBCWeatherTemperature? Temperature { get; init; }
    
    [JsonPropertyName("updateTimestamp")]
    public DateTime UpdateTimestamp { get; init; }
}