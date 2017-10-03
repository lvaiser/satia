﻿using System.Collections.Generic;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessTypeFieldViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }

        public IEnumerable<KeyValuePair<int, string>> SelectList { get; set; }
    }
}