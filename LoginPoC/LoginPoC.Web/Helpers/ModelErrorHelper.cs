using System;
using System.Linq;
using System.Web.Mvc;

namespace LoginPoC.Web.Helpers
{
    public static class ModelErrorHelper
    {
        public static ModelErrorCollection AsModelErrorCollection(this string message)
        {
            var collection = new ModelErrorCollection();

            collection.Add(message);

            return collection;
        }

        public static ModelErrorCollection AsModelErrorCollection(this Exception ex)
        {
            var collection = new ModelErrorCollection();

            collection.Add(ex);

            return collection;
        }

        public static ModelErrorCollection AsModelErrorCollection(this ModelStateDictionary modelState)
        {
            var collection = new ModelErrorCollection();
            modelState.Values.SelectMany(v => v.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList()
                             .ForEach(message => collection.Add(message));

            return collection;
        }
    }
}