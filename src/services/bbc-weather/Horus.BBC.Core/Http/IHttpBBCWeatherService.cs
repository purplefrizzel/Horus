using Horus.BBC.Core.Http.Models;

namespace Horus.BBC.Core.Http;

public interface IHttpBBCWeatherService
{
    public Task<BBCWeatherObservations> GetWeatherObservationsAsync(string locationId, string language = "en");
}