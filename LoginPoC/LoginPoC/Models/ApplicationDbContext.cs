using LoginPoC.Models.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LoginPoC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string connectionstring)
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<ProcessType.ProcessType> ProcessTypes { get; set; }
        public IDbSet<Country> Countries { get; set; }
    }
}