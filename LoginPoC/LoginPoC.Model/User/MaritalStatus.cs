using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Model.User
{
    public enum MaritalStatus
    {
        [Display(Name = "Soltero")]
        Single = 1,
        [Display(Name = "Casado")]
        Married = 2,
        [Display(Name = "Divorciado")]
        Divorced = 3,
        [Display(Name = "Viudo")]
        Widowed = 4,
        [Display(Name = "Otro")]
        Other = 5
    }
}