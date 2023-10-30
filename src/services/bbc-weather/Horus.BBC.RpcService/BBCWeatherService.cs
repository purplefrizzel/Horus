using Horus.BBC.Core.Http;
using Horus.BBC.Core.Models;
using Horus.BBC.Core.Services;
using ProtoBuf.Grpc;

namespace Horus.BBC.RpcService;

public class BBCWeatherService : IBBCWeatherService
{
    private readonly IHttpBBCWeatherService _httpBBCWeatherService;
    
    public BBCWeatherService(IHttpBBCWeatherService httpBBCWeatherService)
    {
        _httpBBCWeatherService = httpBBCWeatherService;
    }

    public async Task<BBCWeatherForecast> GetWeatherForecastAsync(BBCWeatherObservationsRequest request, CallContext context = default)
    {
        throw new NotImplementedException();
    }
}