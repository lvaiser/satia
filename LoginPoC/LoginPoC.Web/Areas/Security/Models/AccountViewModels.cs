using LoginPoC.Model.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Web.Areas.Security.Models
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required(ErrorMessage = "El correo es requrido.")]
		[EmailAddress(ErrorMessage = "Debe ingresar un correo electronico valido.")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Su nombre es requerido.")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Su apellido es requerido.")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Phone Number")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Display(Name = "Birth Date")]
		[DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha con formato dd/mm/yyyy")]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public System.DateTime? BirthDate { get; set; }

		[Display(Name = "Gender")]
		public Gender? Gender { get; set; }

		[Display(Name = "Marital Status")]
		public MaritalStatus? MaritalStatus { get; set; }

		[Display(Name = "Country")]
		public Country Country { get; set; }
	   
		[Display(Name = "State or province")]
		public string StateProvince { get; set; }

		[Display(Name = "City")]
		public string City { get; set; }

		[Display(Name = "Address")]
		public string Address { get; set; }

		[Display(Name = "Occupation")]
		public string Occupation { get; set; }

		[Display(Name = "Can Read")]
		public bool CanRead { get; set; }

		[Display(Name = "Language")]
		public string Language { get; set; }

		public System.Web.Mvc.SelectList Countries { get; set; }
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl { get; set; }
	}

	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}

	public class VerifyCodeViewModel
	{
		[Required]
		public string Provider { get; set; }

		[Required]
		[Display(Name = "Code")]
		public string Code { get; set; }
		public string ReturnUrl { get; set; }

		[Display(Name = "Remember this browser?")]
		public bool RememberBrowser { get; set; }

		public bool RememberMe { get; set; }
	}

	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}

	public class LoginViewModel
	{
		[Required(ErrorMessage = "Su Email es requerido")]
		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "La contraseña es requerida")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Recordarme")]
		public bool RememberMe { get; set; }
	}

	public class RegisterViewModel : ExternalLoginConfirmationViewModel
	{
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}
