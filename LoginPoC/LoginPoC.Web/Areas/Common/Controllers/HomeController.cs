using System.Web.Mvc;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [RequireHttps]
    [AllowAnonymous]   
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}