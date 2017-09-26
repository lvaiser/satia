using Newtonsoft.Json;

namespace LoginPoC.Model.ProcessType
{
    public class ProcessTypeField
    {
        public int Id { get; set; }

        public string Name { get; set; }
        // public string Description { get; set; } // por ahora no lo pongo pero se podría renderizar como un tooltip con mas info del campo

        public FieldType Type { get; set; }

        public bool IsRequired { get; set; }

        public int ProcessTypeId { get; set; }

        [JsonIgnore]
        public virtual ProcessType ProcessType { get; set; }
    }
}
