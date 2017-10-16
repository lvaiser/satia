using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace LoginPoC.Core.User
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsImpersonating(this IPrincipal principal)
        {
            if (principal == null)
            {
                return false;
            }

            var claimsPrincipal = principal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return false;
            }


            return claimsPrincipal.HasClaim("UserImpersonation", "true");
        }

        public static string GetOriginalUsername(this IPrincipal principal)
        {
            if (principal == null)
            {
                return string.Empty;
            }

            var claimsPrincipal = principal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return string.Empty;
            }

            if (!claimsPrincipal.IsImpersonating())
            {
                return string.Empty;
            }

            var originalUsernameClaim = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == "OriginalUsername");

            if (originalUsernameClaim == null)
            {
                return string.Empty;
            }

            return originalUsernameClaim.Value;
        }
    }
}
