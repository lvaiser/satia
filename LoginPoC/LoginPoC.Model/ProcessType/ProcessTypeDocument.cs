using Newtonsoft.Json;

namespace LoginPoC.Model.ProcessType
{
    public class ProcessTypeDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public int ProcessTypeId { get; set; }

        [JsonIgnore]
        public virtual ProcessType ProcessType { get; set; }
    }
}
