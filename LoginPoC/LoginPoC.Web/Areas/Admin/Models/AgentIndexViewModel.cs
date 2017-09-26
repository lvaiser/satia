using System;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class AgentIndexViewModel
    {
        public string SearchByName { get; set; }

        //TODO: Remplazar el dynamic cuando tengamos la entidad Agent
        public IEnumerable<dynamic> Agents { get; set; }
    }
}