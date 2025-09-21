using System.Text.Json.Serialization;

namespace ECommerce.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CancellationStatus
{
    Pending = 1,
    Approved = 8,
    Rejected = 9
}