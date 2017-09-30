using System.Collections.Generic;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class AgentIndexViewModel
    {
        public string SearchByName { get; set; }

        public IEnumerable<ApplicationUser> Agents { get; set; }
    }
}