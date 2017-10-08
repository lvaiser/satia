using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LoginPoC.Web.Helpers
{
    public static class ModelStateDictionaryHelper
    {
        public static string AllErrorsToString(this ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage);

            return string.Join(". ", errorMessages);
        }

        public static void AddErrors(this ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error);
            }
        }
    }
}