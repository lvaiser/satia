using AutoMapper;
using LoginPoC.Core.Teams;
using LoginPoC.Model.Teams;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;
using LoginPoC.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
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

        // POST: Team/Edit/{id}
        [HttpPost]
        public ActionResult Edit(TeamViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(this.ModelState.AsModelErrorCollection());
                }

                var team = this.mapper.Map<Team>(vm);

                this.teamService.Update(team);

                return this.JsonNet(vm);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.JsonNet(ex.AsModelErrorCollection());
            }
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View("Edit", new TeamViewModel());
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(this.ModelState.AsModelErrorCollection());
                }

                var team = this.mapper.Map<Team>(vm);

                this.teamService.Add(team);

                return this.JsonNet(vm);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.JsonNet(ex.AsModelErrorCollection());
            }
        }

        // GET: Team/Delete/{id}
        public ActionResult Delete(int id)
        {
            var team = this.teamService.GetById(id);
            if (team == null)
            {
                return HttpNotFound("No se encontro el equipo especificado");
            }

            var vm = this.mapper.Map<TeamViewModel>(team);
            return View(vm);
        }

        // POST: Team/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.teamService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.JsonNet(ex.AsModelErrorCollection());
            }
        }
    }
}
