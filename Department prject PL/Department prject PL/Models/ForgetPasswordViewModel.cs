using System.ComponentModel.DataAnnotations;

namespace Department_prject_PL.Models
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required"), EmailAddress]
		public string Email { get; set; }
	}
}
