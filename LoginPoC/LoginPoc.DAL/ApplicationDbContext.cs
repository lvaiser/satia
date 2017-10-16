using LoginPoC.Model.File;
using LoginPoC.Model.Process;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.Teams;
using LoginPoC.Model.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

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
        public IDbSet<ProcessTypeDocument> ProcessTypeDocuments { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<File> Files { get; set; }
        public IDbSet<Process> Processes { get; set; }
        public IDbSet<ProcessField> ProcessFields { get; set; }
        public IDbSet<ProcessDocument> ProcessDocuments { get; set; }
        public IDbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                        .HasMany(t => t.Users)
                        .WithMany(u => u.Teams)
                        .Map(m => m.MapRightKey("UserId")
                                   .MapLeftKey("TeamId")
                                   .ToTable("UsersToTeams"));

            base.OnModelCreating(modelBuilder);
        }
    }
}