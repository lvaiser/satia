using System;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.Process;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessIndexViewModel
    {
        public string SearchByName { get; set; }

        public IEnumerable<ProcessViewModel> Processes { get; set; }
        public IEnumerable<ProcessType> ProcessTypes { get; set; }
    }
}