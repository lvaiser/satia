using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;
using LoginPoC.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
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
		private IMapper mapper;

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public UserController(ApplicationUserManager userManager, IMapper mapper)
		{
			this.UserManager = userManager;
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
		public ActionResult Unsubscribe()
		{
			return View();
		}


		[HttpPost, ActionName("Unsubscribe")]
		public async Task<ActionResult> UnsubscribeConfirmed()
		{
			if (!this.User.IsInRole(ApplicationUserRoles.User))
			{
				throw new Exception("Este usuario no puede desuscribirse");
			}

			var user = await UserManager.FindByIdAsync(this.User.Identity.GetUserId());

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