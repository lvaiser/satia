namespace LoginPoC.Web.Areas.Common.Models
{
    public class ProcessFieldViewModel
    {
        public int Id { get; set; }
        public ProcessTypeFieldViewModel Type { get; set; }
        public object Value { get; set; }
    }
}