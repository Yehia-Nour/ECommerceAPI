using System.Text.Json.Serialization;

namespace ECommerce.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RefundMethod
{
    Original,
    PayPal,
    Stripe,
    BankTransfer,
    Manual
}