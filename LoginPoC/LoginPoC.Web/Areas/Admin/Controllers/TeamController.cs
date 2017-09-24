using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Administrator)]
    public class TeamController : Controller
    {
        public TeamController()
        {
            
        }

        // GET: ProcessType        
        public ActionResult Index(string name = null)
        {
            //var teams = await this.TeamService.SearchAsync(name);
            var teams = this.GetMockedTeams();
            var vm = new TeamIndexViewModel()
            {
                Teams = teams,
                SearchByName = name
            };

            return View(vm);
        }

        private IEnumerable<dynamic> GetMockedTeams()
        {
            dynamic uno = new ExpandoObject();
            uno.Id = 1;
            uno.Name = "Equipo de gestion de Ciudadanias";

            dynamic dos = new ExpandoObject();
            dos.Id = 2;
            dos.Name = "Equipo de gestion de Tramites X";

            return new List<dynamic>() { uno, dos };
        }
    }
}
