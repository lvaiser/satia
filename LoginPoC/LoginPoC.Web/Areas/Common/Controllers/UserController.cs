using AutoMapper;
using LoginPoC.Core.Process;
using LoginPoC.Core.User;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;
using LoginPoC.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoginPoC.Web.Areas.Common.Controllers
{
	[RequireHttps]
	[Authorize]
	public class UserController : Controller
	{
		private ApplicationUserManager UserManager;
        private IProcessService ProcessService { get; set; }
        private IMapper mapper;

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public UserController(
            ApplicationUserManager userManager,
            IProcessService processService,
            IMapper mapper)
		{
			this.UserManager = userManager;
            this.ProcessService = processService;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> MyAccount()
		{
			var user = await UserManager.FindByIdAsync(this.User.Identity.GetUserId());
			var vm = mapper.Map<UserViewModel>(user);
			return View(vm);
		}

		[HttpPost]
		public async Task<ActionResult> SaveProfile(UserViewModel vm)
		{
			var user = await UserManager.FindByIdAsync(this.User.Identity.GetUserId());

			mapper.Map(vm, user);
			await this.UserManager.UpdateAsync(user);

			return this.JsonNet(vm);
		}

		[HttpGet]
		public async Task<ActionResult> Unsubscribe()
		{
            var pendingProcesses = await this.ProcessService.SearchMyProcessesAsync(string.Empty, this.User.Identity.GetUserId());
            ViewBag.PendingProcessesCount = pendingProcesses.Count(x => x.Status == Model.Process.ProcessStatus.Draft || x.Status == Model.Process.ProcessStatus.Submitted);

            return View();
		}


		[HttpPost, ActionName("Unsubscribe")]
		public async Task<ActionResult> UnsubscribeConfirmed()
		{
			if (!this.User.IsInRole(ApplicationUserRoles.User))
			{
				throw new Exception("Este usuario no puede desuscribirse");
			}

            var userId = this.User.Identity.GetUserId();

            await this.ProcessService.ArchiveProcessesInProgressAsync(userId);

			var user = await UserManager.FindByIdAsync(userId);

			user.Disabled = true;
			await this.UserManager.UpdateAsync(user);

			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home", new { area = "Common" });
		}

		public ActionResult Menu()
		{
			var user = UserManager.FindById(this.User.Identity.GetUserId());
			var vm = mapper.Map<UserViewModel>(user);
			return View(vm);
		}
	}
}