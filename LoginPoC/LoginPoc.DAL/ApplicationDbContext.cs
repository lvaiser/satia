using LoginPoC.DAL.Models.ProcessType;
using LoginPoC.DAL.Models.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LoginPoC.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<ProcessType> ProcessTypes { get; set; }
        public IDbSet<Country> Countries { get; set; }
    }
}