using System.Collections.Generic;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class AgentIndexViewModel
    {
        public string SearchByName { get; set; }

        public IEnumerable<AgentViewModel> Agents { get; set; }
    }
}