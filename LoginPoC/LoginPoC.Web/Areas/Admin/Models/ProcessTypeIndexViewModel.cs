using System.Collections.Generic;
using LoginPoC.Model.ProcessType;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class ProcessTypeIndexViewModel
    {
        public string SearchByName { get; set; }

        public IEnumerable<ProcessType> ProcessTypes { get; set; }
    }
}