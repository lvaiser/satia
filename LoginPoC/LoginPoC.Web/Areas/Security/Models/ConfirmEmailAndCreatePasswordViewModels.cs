namespace LoginPoC.Web.Areas.Security.Models
{
    public class ConfirmEmailAndCreatePasswordViewModel
    {
        public string UserId { get; set; }

        public string ConfirmationCode { get; set; }

        public string Password { get; set; }
    }
}
