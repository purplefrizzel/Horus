using System.Text.Json.Serialization;

namespace Horus.BBC.Core.Http.Models;

public record BBCWeatherObservations
{
    [JsonPropertyName("station")]
    public BBCWeatherStation? Station { get; init; }
    
    [JsonPropertyName("observations")]
    public BBCWeatherObservation[] Observations { get; init; } = Array.Empty<BBCWeatherObservation>();
}