using System.Collections.Generic;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {
            this.Users = new List<AgentViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }
        
        public IEnumerable<AgentViewModel> Users { get; set; }
    }
}