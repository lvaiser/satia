using System;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Admin.Models
{
    public class TeamIndexViewModel
    {
        public string SearchByName { get; set; }

        //TODO: Remplazar el dynamic cuando tengamos la entidad Team
        public IEnumerable<dynamic> Teams { get; set; }
    }
}