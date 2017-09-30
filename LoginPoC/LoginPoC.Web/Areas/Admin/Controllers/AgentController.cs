using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Admin.Models;

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
                Agents = await users.ToListAsync()
            };

            return View(vm);
        }
    }
}
