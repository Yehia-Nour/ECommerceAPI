using ECommerce.Domain.Enums;
using System.Security.Cryptography;

namespace ECommerce.Application.Helpers;

public static class OrderUtility
{
    public static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{RandomNumber(1000, 9999)}";
    }

    private static int RandomNumber(int min, int max)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            return Math.Abs(BitConverter.ToInt32(bytes, 0) % (max - min + 1)) + min;
        }
    }

    public static readonly Dictionary<OrderStatus, List<OrderStatus>> AllowedStatusTransitions = new()
    {
        { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Canceled } },
        { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Canceled } },
        { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Canceled } },
        { OrderStatus.Delivered, new List<OrderStatus>() },
        { OrderStatus.Canceled, new List<OrderStatus>() }
    };

    public static bool CanTransition(OrderStatus current, OrderStatus next)
    {
        return AllowedStatusTransitions.TryGetValue(current, out var allowedStatuses)
               && allowedStatuses.Contains(next);
    }

}
