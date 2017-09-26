﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginPoC.Model.ProcessType
{
    public class ProcessType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string URLVideo { get; set; }

        public ICollection<ProcessTypeField> Fields { get; set; }

        public ProcessType()
        {
            this.Fields = new List<ProcessTypeField>();
        }
    }
}