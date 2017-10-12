using AutoMapper;
using LoginPoC.Core.Process;
using LoginPoC.Core.ProcessType;
using LoginPoC.Core.User;
using LoginPoC.Model.Process;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;
using LoginPoC.Web.Helpers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Net;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessController : Controller
    {
        // GET DbContext from container
        private IProcessTypeService ProcessTypeService { get; set; }
        private IProcessService ProcessService { get; set; }
        private ICountryService CountryService { get; set; }
        private IMapper Mapper { get; set; }

        public ProcessController(
            IProcessTypeService processTypeService, 
            IProcessService processService,
            ICountryService countryService, 
            IMapper mapper)
        {
            this.ProcessTypeService = processTypeService;
            this.ProcessService = processService;
            this.CountryService = countryService;
            this.Mapper = mapper;
        }

        // GET: ProcessType
        [Authorize]
        public async Task<ActionResult> Index(string name = null)
        {
            var processes = await this.ProcessService.SearchAsync(name);
            var vm = new ProcessIndexViewModel()
            {
                Processes = processes,
                SearchByName = name
            };

            return View(vm);
        }

        // GET: Process
        [OverrideAuthorization]
        [Authorize]
        public async Task<ActionResult> MyProcesses(string name = null)
        {
            var processes = await this.ProcessService.SearchMyProcessesAsync(name, User.Identity.GetUserId());
            var processTypes = await this.ProcessTypeService.SearchAsync(name);
            var vm = new ProcessIndexViewModel()
            {
                Processes = processes,
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

        // POST: Process/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Process process)
        {
            if (!ModelState.IsValid || process.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.AllErrorsToString());
            }

            this.ProcessService.Update(process);
            return this.JsonNet(process);
        }

        [OverrideAuthorization]
        [Authorize]
        public async Task<ActionResult> Create(int Id)
        {
            var process = await this.ProcessService.GetByTypeAsync(Id, User.Identity.GetUserId());
            ProcessViewModel model = Mapper.Map<ProcessViewModel>(process);

            foreach (var item in process.Fields)
            {
                switch (item.Type)
                {
                    case FieldType.Gender:
                        model.Fields.Single(f => f.Type == item.Type.ToString()).SelectList = EnumHelper<Gender>.AsKeyValuePairs();
                        break;
                    case FieldType.MaritalStatus:
                        model.Fields.Single(f => f.Type == item.Type.ToString()).SelectList = EnumHelper<MaritalStatus>.AsKeyValuePairs();
                        break;
                    case FieldType.Country:
                        var countryList = new List<KeyValuePair<int, string>>();
                        var countries = await this.CountryService.GetCountriesAsync();
                        foreach (Country country in countries)
	                    {
                            countryList.Add(Mapper.Map<KeyValuePair<int, string>>(country));
                        }

                        model.Fields.Single(f => f.Type == item.Type.ToString()).SelectList = countryList;
                        break;                    
                    default:
                        break;
                }
            }

            List<ProcessDocument> documents = new List<ProcessDocument>();
            foreach (var item in process.Documents)
            {
                documents.Add(Mapper.Map<ProcessDocument>(item));
            }

            return View("Edit", model);
        }

        // POST: Process/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Process process)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.AllErrorsToString());
            }

            if (process.Id != 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
            
            this.ProcessService.Add(process, User.Identity.GetUserId());

            return this.JsonNet(process);
        }
    }
}
