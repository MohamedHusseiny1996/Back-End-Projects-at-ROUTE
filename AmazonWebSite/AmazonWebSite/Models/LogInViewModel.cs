using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AmazonWebSitePL.Models
{
	public class LogInViewModel
	{
		
		[Required(ErrorMessage = "Email is required"), EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required"), PasswordPropertyText, MinLength(6, ErrorMessage = "min charecters is 6")]
		public string Password { get; set; }


		[Required(ErrorMessage = "required")]
		public bool RememberMe { get; set; }

	}
}
