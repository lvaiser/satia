using LoginPoC.Model.ProcessType;
using Newtonsoft.Json;

namespace LoginPoC.Model.Process
{
    public class ProcessField
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool IsRequired { get; set; }

        [JsonIgnore]
        public virtual Process Process { get; set; }        
    }
}
