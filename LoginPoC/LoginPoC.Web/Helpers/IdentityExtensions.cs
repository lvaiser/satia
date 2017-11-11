using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;

namespace LoginPoC.Web.Helpers
{
    public static class IdentityExtensions
    {
        public static Boolean GetCanRead(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CanRead");
            // Test for null to avoid issues during local testing
            return (claim != null) ? Boolean.Parse(claim.Value) : true;
        }
    }
}