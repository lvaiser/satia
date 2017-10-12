using LoginPoC.Model.ProcessType;
using Newtonsoft.Json;

namespace LoginPoC.Model.Process
{
    public class ProcessDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool IsAvailable { get; set; }
        public int ProcessId { get; set; }

        [JsonIgnore]
        public virtual Process Process { get; set; }        
    }
}