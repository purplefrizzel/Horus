using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Horus.BBC.Core.Http;

public static class HttpServiceExtensions
{
    public static void AddHttpBBCWeatherService(this IServiceCollection services)
    {
        services.AddHttpClient("bbc-weather-api", client =>
        {
            client.BaseAddress = new Uri("https://weather-broker-cdn.api.bbci.co.uk/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Horus.BBC.Core", "1.0"));
        });
        
        services.AddScoped<IHttpBBCWeatherService, HttpBBCWeatherService>();
    }
}