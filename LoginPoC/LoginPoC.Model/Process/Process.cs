using LoginPoC.Model.Process;
using LoginPoC.Model.User;
using System;
using System.Collections.Generic;

namespace LoginPoC.Model.Process
{
    public class Process
    {
        public int Id { get; set; }
        public ProcessType.ProcessType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public ProcessStatus Status { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime? ReviewDate { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser AssignedAgent { get; set; }

        public IEnumerable<ProcessField> Fields { get; set; }
        public IEnumerable<ProcessDocument> Documents { get; set; }
    }
}
