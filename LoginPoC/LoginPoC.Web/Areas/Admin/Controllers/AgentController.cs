using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Core.ProcessType;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class AgentController : Controller
    {
        public AgentController()
        {
            
        }

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }       
    }
}
