using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;
using LoginPoC.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Administrator)]
    public class AgentController : Controller
    {
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IMapper mapper;

        public AgentController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        // GET: Agent        
        public async Task<ActionResult> Index(string name = null)
        {
            var agentRole = await this.roleManager.FindByNameAsync(ApplicationUserRoles.Agent);
            var users = this.userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == agentRole.Id));

            if (!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(u => u.FirstName.Contains(name)
                                      || u.LastName.Contains(name)
                                      || u.UserName.Contains(name));
            }

            var vm = new AgentIndexViewModel()
            {
                SearchByName = name,
                Agents = this.mapper.Map<List<AgentViewModel>>(await users.ToListAsync())
            };

            if (this.Request.IsAjaxRequest())
            {
                return this.JsonNet(vm);
            }

            return View(vm);
        }

        //
        // GET: /Agent/Register
        public ActionResult Register()
        {
            return View("Edit", new AgentViewModel());
        }

        //
        // POST: /Agetn/Register
        [HttpPost]
        public async Task<ActionResult> Register(AgentViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(this.ModelState.AsModelErrorCollection());
                }

                var existing = await this.userManager.FindByEmailAsync(vm.Email);
                if (existing != null)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return this.JsonNet("Ya existe un usuario con el email especificado".AsModelErrorCollection());
                }

                var user = Mapper.Map<ApplicationUser>(vm);

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddErrors(result);
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(ModelState.AsModelErrorCollection());
                }

                await userManager.AddToRoleAsync(user.Id, ApplicationUserRoles.Agent);
                await SendEmailConfirmationTokenToAgentAsync(user.Id, "Confirmar tu cuenta");

                return this.JsonNet(Mapper.Map<AgentViewModel>(user));
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.JsonNet(ex.AsModelErrorCollection());
            }
        }

        // GET: Agent/Edit
        public async Task<ActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound("No se encontro el agente especificado.");
            }

            var vm = mapper.Map<AgentViewModel>(user);

            return View(vm);
        }

        // POST: Agent/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(AgentViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(this.ModelState.AsModelErrorCollection());
                }

                var user = await userManager.FindByIdAsync(vm.Id);
                mapper.Map(vm, user);

                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddErrors(result);
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.JsonNet(this.ModelState.AsModelErrorCollection());
                }

                return this.JsonNet(vm);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.JsonNet(ex.AsModelErrorCollection());
            }
        }

        private async Task<string> SendEmailConfirmationTokenToAgentAsync(string userID, string subject)
        {
            string code = await userManager.GenerateEmailConfirmationTokenAsync(userID);

            var callbackUrl = Url.Action("ConfirmEmailAndCreatePassword", "Account", new { area = "Security", userId = userID, code = code }, protocol: Request.Url.Scheme);
            await userManager.SendEmailAsync(userID, subject, "Confirma tu cuenta y crea una contraseña haciendo <a href=\"" + callbackUrl + "\">click aquí</a>");

            return callbackUrl;
        }
    }
}
