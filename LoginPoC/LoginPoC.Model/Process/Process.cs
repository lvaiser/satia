using LoginPoC.Model.Process;
using LoginPoC.Model.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LoginPoC.Model.Process
{
    public class Process
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public ProcessStatus Status { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string CreatorId { get; set; }
        public string AssignedAgentId { get; set; }

        public ICollection<ProcessField> Fields { get; set; }
        public ICollection<ProcessDocument> Documents { get; set; }

        [JsonIgnore]
        public virtual ProcessType.ProcessType Type { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser Creator { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser AssignedAgent { get; set; }
    }
}
