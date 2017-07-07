namespace LoginPoC.App_Start
{
    using LoginPoC.Models;
    using LoginPoC.Models.User;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Owin;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        public static Container Initialize(IAppBuilder app)
        {
            var container = GetInitializeContainer(app);

            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static Container GetInitializeContainer(IAppBuilder app)
        {
            var container = new Container();
            // Set the scoped lifestyle one directly after creating the container
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.RegisterSingleton<IAppBuilder>(app);

            container.Register<IAuthenticationManager>(() =>
                AdvancedExtensions.IsVerifying(container)
                    ? new OwinContext(new Dictionary<string, object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped); 

            container.Register<SignInManager<ApplicationUser, string>, ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);

            container.Register<ApplicationDbContext>(() => new ApplicationDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString), Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(() => 
                new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()), Lifestyle.Scoped);

            container.RegisterInitializer<ApplicationUserManager>(
                manager => InitializeUserManager(manager, app));

            container.RegisterMvcControllers(
                    Assembly.GetExecutingAssembly());

            return container;
        }

        private static void InitializeUserManager(
            ApplicationUserManager manager, IAppBuilder app)
        {
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider =
                 app.GetDataProtectionProvider();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                 new DataProtectorTokenProvider<ApplicationUser>(
                  dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}