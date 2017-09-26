using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;

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
        public ActionResult Index(string name = null)
        {
            //var agents = await this.AgentService.SearchAsync(name);
            var agents = this.GetMockedAgents();
            var vm = new AgentIndexViewModel()
            {
                Agents = agents,
                SearchByName = name
            };

            return View(vm);
        }

        private IEnumerable<dynamic> GetMockedAgents()
        {
            dynamic uno = new ExpandoObject();
            uno.Id = 1;
            uno.Name = "Sabrina Gallo";

            dynamic dos = new ExpandoObject();
            dos.Id = 1;
            dos.Name = "Marcos Romero";

            dynamic tres = new ExpandoObject();
            tres.Id = 1;
            tres.Name = "Martin Cachimayo";

            return new List<dynamic>() { uno, dos, tres };
        }
    }
}
