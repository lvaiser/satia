using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using LoginPoC.Model.File;

namespace LoginPoC.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public IDbSet<ProcessType> ProcessTypes { get; set; }
        public IDbSet<ProcessTypeField> ProcessTypeFields { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<File> Files { get; set; }
    }
}