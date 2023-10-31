using System.ServiceModel;
using Horus.BBC.Core.Models;
using ProtoBuf.Grpc;

namespace Horus.BBC.Core.Services;

[ServiceContract]
public interface IBBCWeatherService
{
    [OperationContract]
    Task<BBCWeatherForecast> GetWeatherForecastAsync(BBCWeatherObservationsRequest request, CallContext context = default);
}