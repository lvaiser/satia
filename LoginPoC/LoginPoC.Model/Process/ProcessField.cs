using LoginPoC.Model.ProcessType;
using Newtonsoft.Json;

namespace LoginPoC.Model.Process
{
    public class ProcessField
    {
        public int Id { get; set; }
        public ProcessTypeField Type { get; set; }
        public object Value { get; set; }

        [JsonIgnore]
        public virtual Process Process { get; set; }
    }
}
