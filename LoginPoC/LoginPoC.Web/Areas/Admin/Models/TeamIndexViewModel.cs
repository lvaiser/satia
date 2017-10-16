using System.Collections.Generic;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class TeamIndexViewModel
    {
        public TeamIndexViewModel()
        {
            this.Teams = new List<TeamViewModel>();
        }

        public string SearchByName { get; set; }
        
        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}