using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Core.ProcessType;
using LoginPoC.Web.Areas.Admin.Models;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Web.Areas.Common.Models;
using System.Dynamic;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessController : Controller
    {
        public ProcessController()
        {  }

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

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult MyProcesses(string name = null)
        {
            //var process = await this.ProcessService.SearchMyProcessesAsync(name);
            var process = this.GetMockedProcesses();
            var vm = new ProcessIndexViewModel()
            {
                Processes = process,
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

        private IEnumerable<dynamic> GetMockedProcesses()
        {
            dynamic uno = new ExpandoObject();
            uno.Id = 1;
            uno.Name = "Residencia";
            uno.Status = "En Proceso";

            dynamic dos = new ExpandoObject();
            dos.Id = 1;
            dos.Name = "Ciudadania";
            dos.Status = "Finalizado";

            dynamic tres = new ExpandoObject();
            tres.Id = 1;
            tres.Name = "Tramite X";
            tres.Status = "Rechazado";

            return new List<dynamic>() { uno, dos, tres };
        }
    }
}
