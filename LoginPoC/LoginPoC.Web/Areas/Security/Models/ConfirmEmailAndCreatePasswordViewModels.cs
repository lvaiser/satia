using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Web.Areas.Security.Models
{
    public class ConfirmEmailAndCreatePasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
