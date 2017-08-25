﻿using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;
using LoginPoC.Web.Helpers;
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
    }
}