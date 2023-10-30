using System.Runtime.Serialization;

namespace Horus.BBC.Core.Models;

[DataContract]
public class BBCWeatherObservationsRequest
{
    [DataMember(Order = 1)]
    public string LocationId { get; set; } = string.Empty;
}