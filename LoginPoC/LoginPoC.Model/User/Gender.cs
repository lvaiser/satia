using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Model.User
{
    public enum Gender
    {
        [Display(Name = "Hombre")]
        Male = 1,
        [Display(Name = "Mujer")]
        Female = 2
    }
}