using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPoC.Model.ProcessType
{
    public class ProcessTypeField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // public string Description { get; set; } // por ahora no lo pongo pero se podría renderizar como un tooltip con mas info del campo
        public FieldType Type { get; set; }
        public bool IsRequired { get; set; }

        public virtual ProcessType ProcessType { get; set; }
    }
}
