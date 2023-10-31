using System.Text.Json;
using Horus.BBC.Core.Http.Models;
using Serilog;

namespace Horus.BBC.Core.Http;

public class HttpBBCWeatherService : IHttpBBCWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    
    public HttpBBCWeatherService(IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("bbc-weather-api");
    }


    public async Task<BBCWeatherObservations> GetWeatherObservationsAsync(string locationId, string language = "en")
    {
        var url = $"{language}/observation/{locationId}";
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.Error("Failed to get weather observations for location {LocationId} with status code {StatusCode}", locationId, response.StatusCode);
            throw new HttpRequestException($"Failed to get weather observations for location {locationId} with status code {response.StatusCode}");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var bbcWeatherObservations = JsonSerializer.Deserialize<BBCWeatherObservations>(json);
        
        return bbcWeatherObservations ?? throw new HttpRequestException($"Failed to get weather observations for location {locationId}");
    }
}