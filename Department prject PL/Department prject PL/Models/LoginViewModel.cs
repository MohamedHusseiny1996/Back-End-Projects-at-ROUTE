using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Department_prject_PL.Models
{
    public class LoginViewModel
    {
       

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; }
       

        [Required(ErrorMessage = "Password is required"), PasswordPropertyText]
        public string Password { get; set; }


        
        public bool RememberMe { get; set; }
    }
}
