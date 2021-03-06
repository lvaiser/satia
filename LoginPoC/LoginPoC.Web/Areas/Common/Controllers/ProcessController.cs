﻿using AutoMapper;
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
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoginPoC.Web.Areas.Common.Controllers
{
	[Authorize]
	public class ProcessController : Controller
	{
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

		// GET: Process
		[OverrideAuthorization]
		[Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
		public async Task<ActionResult> Index(string name = null)
		{
			var processes = (await this.ProcessService.SearchNotDraftAsync(name))
													  .ToList();
			processes.Sort(ByStatusAndDate);

			var vm = new ProcessIndexViewModel()
			{
				Processes = processes.Select(x => Mapper.Map<ProcessViewModel>(x)),
				SearchByName = name
			};

			return View(vm);
		}

		// GET: Process
		public async Task<ActionResult> MyProcesses(string name = null)
		{
			var processes = await this.ProcessService.SearchMyProcessesAsync(name, User.Identity.GetUserId());
			var processTypes = await this.ProcessTypeService.SearchAsync(null, User.Identity.GetUserId());
			var vm = new ProcessIndexViewModel()
			{
				Processes = processes.Select(x => Mapper.Map<ProcessViewModel>(x)),
				ProcessTypes = processTypes,
				SearchByName = name
			};

			return View(vm);
		}

		public async Task<ActionResult> Edit(int id)
		{
			var process = this.ProcessService.GetById(id);
			ProcessViewModel model = Mapper.Map<ProcessViewModel>(process);

			await FillSelectLists(process, model);

			return View(model);
		}

		// POST: Process/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public ActionResult Edit(ProcessViewModel process)
		{
			if (!ModelState.IsValid || process.Id == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.AllErrorsToString());
			}

			Process model = Mapper.Map<Process>(process);
			this.ProcessService.Update(model);
			process = Mapper.Map<ProcessViewModel>(model);

			return this.JsonNet(process);
		}

		public async Task<ActionResult> Create(int Id)
		{
			var process = await this.ProcessService.GetByTypeAsync(Id, User.Identity.GetUserId());
			ProcessViewModel model = Mapper.Map<ProcessViewModel>(process);

			await FillSelectLists(process, model);

			List<ProcessDocument> documents = new List<ProcessDocument>();
			foreach (var item in process.Documents)
			{
				documents.Add(Mapper.Map<ProcessDocument>(item));
			}

			return View("Edit", model);
		}

		private async Task FillSelectLists(Process process, ProcessViewModel model)
		{
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
			}

		// POST: Process/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public ActionResult Create(ProcessViewModel process)
		{
			if (!ModelState.IsValid)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.AllErrorsToString());
			}

			if (process.Id != 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Conflict);
			}

			Process model = Mapper.Map<Process>(process);
			this.ProcessService.Add(model, User.Identity.GetUserId());
			process = Mapper.Map<ProcessViewModel>(model);

			return this.JsonNet(process);
		}

		// GET: Process/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Process process = this.ProcessService.GetById(id.Value);
			if (process == null)
			{
				return HttpNotFound();
			}

			ProcessViewModel model = Mapper.Map<ProcessViewModel>(process);
			return View(model);
		}

		// POST: Process/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			this.ProcessService.Delete(id);
			return RedirectToAction("MyProcesses");
		}

		// GET: Process/Assign/{id}?userId={userId}
		public async Task<ActionResult> Assign(int id, string userId)
		{
			await this.ProcessService.AssignAgentAsync(id, userId);
			return Redirect(this.Request.UrlReferrer.ToString());
		}

		// GET: Process/Deassign/{id}
		public async Task<ActionResult> Deassign(int id)
		{
			await this.ProcessService.DeassignAsync(id);
			return Redirect(this.Request.UrlReferrer.ToString());
		}

		// POST: Process/Send/{id}
		[HttpPost]
		public async Task<ActionResult> Send(int id)
		{
			await this.ProcessService.ChangeStatusAsync(id, ProcessStatus.Submitted);
			return this.JsonNet(new { ok = true });
		}

		// POST: Process/Approve/{id}
		[HttpPost]
		public async Task<ActionResult> Approve(int id, string reviewNotes)
		{
			await this.ProcessService.ChangeStatusAsync(id, ProcessStatus.Approved, reviewNotes);
			return this.JsonNet(new { ok = true });
		}

		// POST: Process/Reject/{id}
		[HttpPost]
		public async Task<ActionResult> Reject(int id, string reviewNotes)
		{
			await this.ProcessService.ChangeStatusAsync(id, ProcessStatus.Rejected, reviewNotes);
			return this.JsonNet(new { ok = true });
		}

		private int ByStatusAndDate(Process a, Process b)
		{
			if (a.Status == b.Status)
			{
				if (a.AssignedAgentId == this.User.Identity.GetUserId<string>() && b.AssignedAgentId == this.User.Identity.GetUserId<string>())
					return a.CreationDate.CompareTo(b.CreationDate);

				if (a.AssignedAgentId == this.User.Identity.GetUserId<string>())
					return -1;
				
				if (b.AssignedAgentId == this.User.Identity.GetUserId<string>())
					return 1;

				if (a.AssignedAgentId != null && b.AssignedAgent != null)
					return a.CreationDate.CompareTo(b.CreationDate);

				if (a.AssignedAgentId == null)
					return -1;

				if (b.AssignedAgentId != null)
					return 1;
			}

			return a.Status.CompareTo(b.Status);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
