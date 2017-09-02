namespace LoginPoC.Web.Migrations
{
    using EntityFramework.Seeder;
    using LoginPoC.DAL;
    using LoginPoC.Model.ProcessType;
    using LoginPoC.Model.User;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private bool AddRole(string role, ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole(role));

            return ir.Succeeded;
        }

        private bool AddUser(string email, string password, string firstName, string lastName, string roleName, ApplicationDbContext context)
        {
            IdentityResult ir;
            var um = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.UserName == email))
            {
                var user = new ApplicationUser
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    EmailConfirmed = true,
                    CountryId = 1
                };

                ir = um.Create(user, password);
                if (ir.Succeeded)
                {
                    ir = um.AddToRole(user.Id, roleName);
                }
                else
                {
                    throw new Exception(ir.Errors.First());
                }

                return ir.Succeeded;
            }

            return false;
        }

        private void AddLocations(ApplicationDbContext context)
        {
            context.Countries.SeedFromResource("LoginPoC.Web.Models.SeedData.countries.csv", c => c.Code);
            context.SaveChanges();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            this.AddLocations(context);

            this.AddRole(ApplicationUserRoles.Administrator, context);
            this.AddRole(ApplicationUserRoles.Agent, context);
            this.AddRole(ApplicationUserRoles.User, context);

            this.AddUser("admin@satia.com", "admin123", "Admin", "Admin", ApplicationUserRoles.Administrator, context);
            this.AddUser("agent@satia.com", "agent123", "Saira", "Copahue", ApplicationUserRoles.Agent, context);
            this.AddUser("user@satia.com", "user123", "Martin", "Fernandes", ApplicationUserRoles.User, context);

            context.ProcessTypes.AddOrUpdate(p => p.Name,
                new ProcessType
                {
                    Name = "Ciudadan�a",
                    Description = "Tr�mite de ciudadan�a"
                },
                new ProcessType
                {
                    Name = "Residencia",
                    Description = "Tr�mite de residencia"
                }
            );
        }
    }
}
