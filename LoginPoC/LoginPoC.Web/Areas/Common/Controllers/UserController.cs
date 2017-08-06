using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Web.Areas.Common.Models;
using Microsoft.AspNet.Identity;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [RequireHttps]
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationUserManager UserManager;
        private IMapper mapper;

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
            return View(user);
        }
    }
}