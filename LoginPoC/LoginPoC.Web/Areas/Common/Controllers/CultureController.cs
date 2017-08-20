using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LoginPoC.Web.Helpers;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [RequireHttps]
    public class CultureController : Controller
    {
        [HttpGet]
        public ActionResult ListAll()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                                      .Distinct(new CultureEqualityComparer())
                                      .OrderBy(c => c.DisplayName)
                                      .Select(c => new { DisplayName = c.DisplayName, Id = c.Name });

            return this.JsonNet(cultures);
        }

        private class CultureEqualityComparer : IEqualityComparer<CultureInfo>
        {
            public bool Equals(CultureInfo x, CultureInfo y)
            {
                return x.DisplayName.Equals(y.DisplayName);
            }

            public int GetHashCode(CultureInfo obj)
            {
                return obj.DisplayName.GetHashCode();
            }
        }
    }
}