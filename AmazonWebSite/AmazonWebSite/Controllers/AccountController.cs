using AmazonWebSiteDAL.Entities;
using AmazonWebSitePL.Models;
using Department_prject_PL.Helper;
using Department_prject_PL.Models;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AmazonWebSitePL.Controllers
{
	public class AccountController : Controller
	{
		public Microsoft.AspNetCore.Identity.UserManager<UserApplication> _UserManager { get; set; }
		public SignInManager<UserApplication> _SignInManager { get; set; }

		public AccountController(UserManager<UserApplication> userManager, SignInManager<UserApplication> signInManager)
        {
			_UserManager = userManager;
			_SignInManager = signInManager;
		}

		

		public IActionResult Register()
		{
			return View(new RegistrationViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
		{
			if(ModelState.IsValid)
			{
				UserApplication user = new UserApplication()
				{

					Email = registrationViewModel.Email,
					IsAgree = registrationViewModel.IsAgree,
					UserName = registrationViewModel.Email.Split("@")[0]
					

				};
				
			var result	= await _UserManager.CreateAsync(user,registrationViewModel.Password);
				if (result.Succeeded)
				{
					await _UserManager.AddToRoleAsync(user, "Normal User");
					return RedirectToAction("LogIn", "Account");
				}
				foreach(var error in  result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(registrationViewModel);
		}

		/*LogIN  */

		public IActionResult LogIn()
		{
			return View(new LogInViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _UserManager.FindByEmailAsync(logInViewModel.Email);
				if(user is not null )
				{
				  var result	= await _UserManager.CheckPasswordAsync(user, logInViewModel.Password);
					if(result)
					{
						var LoginResult = await _SignInManager.PasswordSignInAsync(user,logInViewModel.Password, logInViewModel.RememberMe, false);
						if (LoginResult.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					ModelState.AddModelError(string.Empty, "Password is incorrect");
				}
				ModelState.AddModelError(string.Empty, "Email is NotFound");
			}
			return View( logInViewModel);
		}

		public new async Task<IActionResult> SignOut()
		{
			await _SignInManager.SignOutAsync();

			return RedirectToAction("Login", "Account");
		}


        public IActionResult ForgetPassword()
        {


            return View(new ForgetPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _UserManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
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

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var resetPasswordViewModel = new ResetPasswordViewModel() { Email = email, Token = token };

            return View(resetPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _UserManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
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
