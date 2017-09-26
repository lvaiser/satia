using System.Linq;
using System.Web.Mvc;

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
    }
}