using LoginPoC.DAL.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using SimpleInjector;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginPoC.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app, Container container)
        {
            // Configure the user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => container.GetInstance<ApplicationUserManager>());
            app.CreatePerOwinContext(() => container.GetInstance<ApplicationSignInManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Security/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(CreateFacebookAuthOptions());

            app.UseGoogleAuthentication(CreateGoogleAuthenticationOptions());
        }

        private static GoogleOAuth2AuthenticationOptions CreateGoogleAuthenticationOptions()
        {
            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["googleClientId"],
                ClientSecret = ConfigurationManager.AppSettings["googleClientSecret"],
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim(ClaimTypes.Gender, context.User.GetValue("gender").ToString()));
                        return Task.FromResult(0);
                    }
                }
            };

            // default scopes
            googleAuthenticationOptions.Scope.Add("openid");
            googleAuthenticationOptions.Scope.Add("profile");
            googleAuthenticationOptions.Scope.Add("email");

            // additional scope(s)
            googleAuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.me");

            return googleAuthenticationOptions;
        }

        private FacebookAuthenticationOptions CreateFacebookAuthOptions()
        {
            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["facebookAppId"],
                AppSecret = ConfigurationManager.AppSettings["facebookAppSecret"],
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                        return Task.FromResult(true);
                    }
                }
            };

            facebookAuthenticationOptions.Scope.Add("email");
            facebookAuthenticationOptions.Scope.Add("user_birthday");
            facebookAuthenticationOptions.Scope.Add("user_location");

            return facebookAuthenticationOptions;
        }
    }
}