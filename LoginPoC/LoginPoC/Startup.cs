using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginPoC.Startup))]
namespace LoginPoC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
