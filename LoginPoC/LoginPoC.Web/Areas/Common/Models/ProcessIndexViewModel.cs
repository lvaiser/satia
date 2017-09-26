﻿using System;
using System.Collections;
using System.Collections.Generic;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessIndexViewModel
    {
        public string SearchByName { get; set; }

        //TODO: Remplazar el dynamic por Process cuando tengamos esa entidad
        public IEnumerable<dynamic> Processes { get; set; }
    }
}