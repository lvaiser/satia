﻿using LoginPoC.Model.Teams;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginPoC.Model.User
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            this.Teams = new List<Team>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public bool CanRead { get; set; }
        public string CultureName { get; set; }
        public int? PictureId { get; set; }
        public File.File Picture { get; set; }
        public bool Disabled { get; set; }

        public List<Team> Teams { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("CanRead", this.CanRead.ToString()));
            return userIdentity;
        }
    }
}