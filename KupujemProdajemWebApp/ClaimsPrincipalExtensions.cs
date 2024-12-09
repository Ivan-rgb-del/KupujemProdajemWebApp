using System.Security.Claims;

namespace KupujemProdajemWebApp
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                throw new InvalidOperationException("User ID claim is missing.");
            }

            return claim.Value;
        }
    }
}
