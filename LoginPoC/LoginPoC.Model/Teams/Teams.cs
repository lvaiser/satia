using LoginPoC.Model.User;
using System.Collections.Generic;

namespace LoginPoC.Model.Teams
{
    public class Team
    {
        public Team()
        {
            this.Users = new List<ApplicationUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}
