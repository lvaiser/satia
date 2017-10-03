using LoginPoC.Model.ProcessType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessViewModel
    {
        public int Id { get; set; }
        public ProcessType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public ProcessStatus Status { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime ReviewDate { get; set; }

        public IEnumerable<ProcessFieldViewModel> Fields { get; set; }
    }
}