using Department_prject_PL.Helper;
using Department_prject_PL.Models;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Department_prject_PL.Controllers
{
   
    public class AccountController : Controller
    {
        public UserManager<UserApplication> _UserManager { get; set; }
        public SignInManager<UserApplication> _SignInManager { get; }

        public AccountController(UserManager<UserApplication> userManager , SignInManager<UserApplication> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                UserApplication user = new UserApplication()
                {
                    Email = registerViewModel.Email,
                    IsAgree = registerViewModel.IsAgree,
                    UserName = registerViewModel.Email.Split("@")[0],
                    PhoneNumber = registerViewModel.PhoneNumber,
                    
                    
                };

               var result= await _UserManager.CreateAsync(user,registerViewModel.Password);
                if(result.Succeeded)
                return RedirectToAction("Login");

                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
              var user = await _UserManager.FindByEmailAsync(loginViewModel.Email);
                if ( user is not null )
                {
                  var result =await _UserManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (result)
                    {
                       var LoginResult= await _SignInManager.PasswordSignInAsync(user,loginViewModel.Password, loginViewModel.RememberMe,false);
                        if(LoginResult.Succeeded)
                        return RedirectToAction("Index","Home");
                    }
                    ModelState.AddModelError(string.Empty, "Password is not correct");
                }
                ModelState.AddModelError(string.Empty, "Email is not found");
            }
            return View(loginViewModel);
        }

		public new async Task<IActionResult> SignOut()
		{
            await _SignInManager.SignOutAsync();
            
			return RedirectToAction("Login","Account");
		}

		public IActionResult ForgetPassword()
		{
			

			return View(new ForgetPasswordViewModel());
		}

        [HttpPost]
		public  async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			if(ModelState.IsValid)
            {

               var user = await _UserManager.FindByEmailAsync(model.Email);

                if(user is not null)
                {
                  var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {Email= model.Email , Token= token} , Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = ResetPasswordLink,
                        To = model.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox", "Account");
                }
                ModelState.AddModelError("", "Enter a valid email");
            }


			return View(model);
		}

		public  async Task<IActionResult> ResetPassword(string email, string token)
		{
           var resetPasswordViewModel= new ResetPasswordViewModel() { Email = email, Token = token };

			return View(resetPasswordViewModel);
		}
        [HttpPost]
		public  async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
			    var user = await _UserManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
			      var result = await _UserManager.ResetPasswordAsync(user, model.Token , model.Password);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("",error.Description);
                }
                return BadRequest();

            }

			return View(model);
		}



        public IActionResult CheckYourInbox()
        {
            return View();
        }



	}

    
}
 