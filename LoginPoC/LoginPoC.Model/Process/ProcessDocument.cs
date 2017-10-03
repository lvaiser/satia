using LoginPoC.Model.ProcessType;
using Newtonsoft.Json;

namespace LoginPoC.Model.Process
{
    public class ProcessDocument
    {
        public int Id { get; set; }
        public ProcessTypeDocument Document { get; set; }
        public bool IsAvailable { get; set; }

        [JsonIgnore]
        public virtual Process Process { get; set; }
    }
}