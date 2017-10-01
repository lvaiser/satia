using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Core.ProcessType;
using LoginPoC.Core.Process;
using LoginPoC.Model.Process;
using LoginPoC.Web.Areas.Admin.Models;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Web.Areas.Common.Models;
using System.Dynamic;
using System.Threading.Tasks;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessController : Controller
    {
        // GET DbContext from container
        private IProcessTypeService ProcessTypeService { get; set; }
        private IProcessService ProcessService { get; set; }

        public ProcessController(IProcessTypeService processTypeService, IProcessService processService)
        {
            this.ProcessTypeService = processTypeService;
            this.ProcessService = processService;
        }

        // GET: ProcessType
        [Authorize]
        public ActionResult Index(string name = null)
        {
            //var process = await this.ProcessService.SearchAsync(name);
            var process = this.GetMockedProcesses();
            var vm = new ProcessIndexViewModel()
            {
                Processes = process,
                SearchByName = name
            };

            return View(vm);
        }

        // GET: Process
        [OverrideAuthorization]
        [Authorize]
        public async Task<ActionResult> MyProcesses(string name = null)
        {
            //var process = await this.ProcessService.SearchMyProcessesAsync(name);
            var process = this.GetMockedProcesses();
            var processTypes = await this.ProcessTypeService.SearchAsync(name);
            var vm = new ProcessIndexViewModel()
            {
                Processes = process,
                ProcessTypes = processTypes,
                SearchByName = name
            };

            return View(vm);
        }

        [OverrideAuthorization]
        [Authorize]
        public ActionResult Edit(int Id)
        {
            return View();
        }

        [OverrideAuthorization]
        [Authorize]
        public async Task<ActionResult> Create(int Id)
        {
            var process = this.ProcessService.GetByTypeAsync(Id, User.Identity.GetUserId());
            return View("Edit", process);
        }

        private IEnumerable<Process> GetMockedProcesses()
        {

            return new List<Process>() { };
        }
    }
}
