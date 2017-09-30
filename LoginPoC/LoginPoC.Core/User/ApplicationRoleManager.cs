using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoginPoC.Core.User
{
    // Configure the application role manager used in this application. RoleManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }
    }
}
