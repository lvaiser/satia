using AutoMapper;
using LoginPoC.Core.Teams;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Administrator)]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IMapper mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            this.teamService = teamService;
            this.mapper = mapper;
        }

        // GET: Team
        public async Task<ActionResult> Index(string name = null)
        {
            var teams = await this.teamService.SearchAsync(name);
            var vm = new TeamIndexViewModel()
            {
                Teams = this.mapper.Map<List<TeamViewModel>>(teams),
                SearchByName = name
            };

            return View(vm);
        }

        // GET: Team/Edit/{id}
        public ActionResult Edit(int id)
        {
            var team = this.teamService.GetById(id);
            if (team == null)
            {
                return HttpNotFound("No se encontro el equipo especificado");
            }

            var vm = this.mapper.Map<TeamViewModel>(team);
            return View(vm);
        }
    }
}
