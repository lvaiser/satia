using LoginPoC.Model.ProcessType;

namespace LoginPoC.Model.Process
{
    public class ProcessField
    {
        public int Id { get; set; }
        public ProcessTypeField Type { get; set; }
        public object Value { get; set; }

        public virtual Process Process { get; set; }
    }
}
