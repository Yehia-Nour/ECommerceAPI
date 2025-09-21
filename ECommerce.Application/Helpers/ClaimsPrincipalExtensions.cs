using System.Security.Claims;

namespace ECommerce.Application.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetCustomerId(this ClaimsPrincipal user)
    {
        var customerIdStr = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(customerIdStr, out int customerId))
            throw new Exception("Invalid or missing customer ID in token.");

        return customerId;
    }
}
