using System.Security.Claims;

namespace ECommerce.Application.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetCustomerId(this ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claim == null || !int.TryParse(claim.Value, out int customerId))
            throw new Exception("Invalid or missing customer ID in token.");

        return customerId;
    }
}
