using System;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class ProcessTypeIndexViewModel
    {
        public string SearchByName { get; set; }

        public IEnumerable<ProcessType> ProcessTypes { get; set; }
    }
}