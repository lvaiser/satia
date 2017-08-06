using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LoginPoC.Web.Helpers
{
    public static class ControllerExtensions
    {
        public static ActionResult JsonNet(this Controller controller, object obj)
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(obj),
                ContentEncoding = Encoding.UTF8
            };
        }
    }
}