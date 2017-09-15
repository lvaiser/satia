using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Core.ProcessType;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessController : Controller
    {
        public ProcessController()
        {
            
        }

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult MyProcesses()
        {
            return View();
        }
    }
}
