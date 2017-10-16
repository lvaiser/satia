using LoginPoC.Model.Process;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using System;
using System.Collections.Generic;
using System.Web;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessViewModel
    {
        public int Id { get; set; }
        public ProcessType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime? ReviewDate { get; set; }
        public ApplicationUser AssignedAgent { get; set; }
        public ApplicationUser Creator { get; set; }

        public IEnumerable<ProcessFieldViewModel> Fields { get; set; }
        public IEnumerable<ProcessDocument> Documents { get; set; }
    }
}